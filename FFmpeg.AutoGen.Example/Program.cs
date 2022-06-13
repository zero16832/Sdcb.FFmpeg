﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FFmpeg.AutoGen.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
            Console.WriteLine("Running in {0}-bit mode.", Environment.Is64BitProcess ? "64" : "32");

            Console.WriteLine($"FFmpeg version info: {ffmpeg.av_version_info()}");

            SetupLogging();
            ConfigureHWDecoder(out var deviceType);

            Console.WriteLine("Decoding...");
            DecodeAllFramesToImages(deviceType);

            Console.WriteLine("Encoding...");
            EncodeImagesToH264();
        }

        private static void ConfigureHWDecoder(out AVHWDeviceType HWtype)
        {
            HWtype = AVHWDeviceType.None;
            Console.WriteLine("Use hardware acceleration for decoding?[n]");
            var key = Console.ReadLine();
            var availableHWDecoders = new Dictionary<int, AVHWDeviceType>();

            if (key == "y")
            {
                Console.WriteLine("Select hardware decoder:");
                var type = AVHWDeviceType.None;
                var number = 0;

                while ((type = ffmpeg.av_hwdevice_iterate_types(type)) != AVHWDeviceType.None)
                {
                    Console.WriteLine($"{++number}. {type}");
                    availableHWDecoders.Add(number, type);
                }

                if (availableHWDecoders.Count == 0)
                {
                    Console.WriteLine("Your system have no hardware decoders.");
                    HWtype = AVHWDeviceType.None;
                    return;
                }

                var decoderNumber = availableHWDecoders
                    .SingleOrDefault(t => t.Value == AVHWDeviceType.Dxva2).Key;
                if (decoderNumber == 0)
                    decoderNumber = availableHWDecoders.First().Key;
                Console.WriteLine($"Selected [{decoderNumber}]");
                int.TryParse(Console.ReadLine(), out var inputDecoderNumber);
                availableHWDecoders.TryGetValue(inputDecoderNumber == 0 ? decoderNumber : inputDecoderNumber,
                    out HWtype);
            }
        }

        private static unsafe void SetupLogging()
        {
            ffmpeg.av_log_set_level(ffmpeg.AV_LOG_VERBOSE);

            // do not convert to local function
            av_log_set_callback_callback logCallback = (p0, level, format, vl) =>
            {
                if (level > ffmpeg.av_log_get_level()) return;

                var lineSize = 1024;
                var lineBuffer = stackalloc byte[lineSize];
                var printPrefix = 1;
                ffmpeg.av_log_format_line(p0, level, format, vl, lineBuffer, lineSize, &printPrefix);
                var line = Marshal.PtrToStringAnsi((IntPtr) lineBuffer);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(line);
                Console.ResetColor();
            };

            ffmpeg.av_log_set_callback(logCallback);
        }

        private static unsafe void DecodeAllFramesToImages(AVHWDeviceType HWDevice)
        {
            // decode all frames from url, please not it might local resorce, e.g. string url = "../../sample_mpeg4.mp4";
            var url = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4"; // be advised this file holds 1440 frames
            using var vsd = new VideoStreamDecoder(url, HWDevice);

            Console.WriteLine($"codec name: {vsd.CodecName}");

            var info = vsd.GetContextInfo();
            info.ToList().ForEach(x => Console.WriteLine($"{x.Key} = {x.Value}"));

            var sourceSize = vsd.FrameSize;
            var sourcePixelFormat = HWDevice == AVHWDeviceType.None
                ? vsd.PixelFormat
                : GetHWPixelFormat(HWDevice);
            var destinationSize = sourceSize;
            var destinationPixelFormat = AVPixelFormat.Bgr24;
            using var vfc =
                new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat);

            var frameNumber = 0;

            while (vsd.TryDecodeNextFrame(out var frame))
            {
                var convertedFrame = vfc.Convert(frame);

                using (var bitmap = new Bitmap(convertedFrame.width,
                    convertedFrame.height,
                    convertedFrame.linesize[0],
                    PixelFormat.Format24bppRgb,
                    (IntPtr) convertedFrame.data[0]))
                    bitmap.Save($"frame.{frameNumber:D8}.jpg", ImageFormat.Jpeg);

                Console.WriteLine($"frame: {frameNumber}");
                frameNumber++;
            }
        }

        private static AVPixelFormat GetHWPixelFormat(AVHWDeviceType hWDevice)
        {
            return hWDevice switch
            {
                AVHWDeviceType.None => AVPixelFormat.None,
                AVHWDeviceType.Vdpau => AVPixelFormat.Vdpau,
                AVHWDeviceType.Cuda => AVPixelFormat.Cuda,
                AVHWDeviceType.Vaapi => AVPixelFormat.Vaapi,
                AVHWDeviceType.Dxva2 => AVPixelFormat.Nv12,
                AVHWDeviceType.Qsv => AVPixelFormat.Qsv,
                AVHWDeviceType.Videotoolbox => AVPixelFormat.Videotoolbox,
                AVHWDeviceType.D3d11va => AVPixelFormat.Nv12,
                AVHWDeviceType.Drm => AVPixelFormat.DrmPrime,
                AVHWDeviceType.Opencl => AVPixelFormat.Opencl,
                AVHWDeviceType.Mediacodec => AVPixelFormat.Mediacodec,
                _ => AVPixelFormat.None
            };
        }

        private static unsafe void EncodeImagesToH264()
        {
            var frameFiles = Directory.GetFiles(".", "frame.*.jpg").OrderBy(x => x).ToArray();
            var fistFrameImage = Image.FromFile(frameFiles.First());

            var outputFileName = "out.h264";
            var fps = 25;
            var sourceSize = fistFrameImage.Size;
            var sourcePixelFormat = AVPixelFormat.Bgr24;
            var destinationSize = sourceSize;
            var destinationPixelFormat = AVPixelFormat.Yuv420p;
            using var vfc =
                new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat);

            using var fs = File.Open(outputFileName, FileMode.Create);

            using var vse = new H264VideoStreamEncoder(fs, fps, destinationSize);

            var frameNumber = 0;

            foreach (var frameFile in frameFiles)
            {
                byte[] bitmapData;

                using (var frameImage = Image.FromFile(frameFile))
                using (var frameBitmap = frameImage is Bitmap bitmap ? bitmap : new Bitmap(frameImage))
                    bitmapData = GetBitmapData(frameBitmap);

                fixed (byte* pBitmapData = bitmapData)
                {
                    var data = new byte_ptrArray8 { [0] = pBitmapData };
                    var linesize = new int_array8 { [0] = bitmapData.Length / sourceSize.Height };
                    var frame = new AVFrame
                    {
                        data = data,
                        linesize = linesize,
                        height = sourceSize.Height
                    };
                    var convertedFrame = vfc.Convert(frame);
                    convertedFrame.pts = frameNumber * fps;
                    vse.Encode(convertedFrame);
                }

                Console.WriteLine($"frame: {frameNumber}");
                frameNumber++;
            }
        }

        private static byte[] GetBitmapData(Bitmap frameBitmap)
        {
            var bitmapData = frameBitmap.LockBits(new Rectangle(Point.Empty, frameBitmap.Size),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            try
            {
                var length = bitmapData.Stride * bitmapData.Height;
                var data = new byte[length];
                Marshal.Copy(bitmapData.Scan0, data, 0, length);
                return data;
            }
            finally
            {
                frameBitmap.UnlockBits(bitmapData);
            }
        }
    }
}
