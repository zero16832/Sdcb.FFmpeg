// This file was genereated from Sdcb.FFmpeg.AutoGen, DO NOT CHANGE DIRECTLY.
#nullable enable
using Sdcb.FFmpeg.Common;
using Sdcb.FFmpeg.Codecs;
using Sdcb.FFmpeg.Raw;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sdcb.FFmpeg.Formats;

/// <summary>
/// <para>Bytestream IO Context. New public fields can be added with minor version bumps. Removal, reordering and changes to existing public fields require a major version bump. sizeof(AVIOContext) must not be used outside libav*.</para>
/// <see cref="AVIOContext" />
/// </summary>
public unsafe partial class IOContext : SafeHandle
{
    protected AVIOContext* _ptr => (AVIOContext*)handle;
    
    public static implicit operator AVIOContext*(IOContext data) => data != null ? (AVIOContext*)data.handle : null;
    
    protected IOContext(AVIOContext* ptr, bool isOwner): base(NativeUtils.NotNull((IntPtr)ptr), isOwner)
    {
    }
    
    public static IOContext FromNative(AVIOContext* ptr, bool isOwner) => new IOContext(ptr, isOwner);
    
    public static IOContext? FromNativeOrNull(AVIOContext* ptr, bool isOwner) => ptr == null ? null : new IOContext(ptr, isOwner);
    
    public override bool IsInvalid => handle == IntPtr.Zero;
    
    /// <summary>
    /// <para>original type: AVClass*</para>
    /// <para>A class for private options.</para>
    /// <see cref="AVIOContext.av_class" />
    /// </summary>
    public FFmpegClass AvClass
    {
        get => FFmpegClass.FromNative(_ptr->av_class);
        set => _ptr->av_class = (AVClass*)value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <para>Start of the buffer.</para>
    /// <see cref="AVIOContext.buffer" />
    /// </summary>
    public IntPtr Buffer
    {
        get => (IntPtr)_ptr->buffer;
        set => _ptr->buffer = (byte*)value;
    }
    
    /// <summary>
    /// <para>Maximum buffer size</para>
    /// <see cref="AVIOContext.buffer_size" />
    /// </summary>
    public int BufferSize
    {
        get => _ptr->buffer_size;
        set => _ptr->buffer_size = value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <para>Current position in the buffer</para>
    /// <see cref="AVIOContext.buf_ptr" />
    /// </summary>
    public IntPtr BufPtr
    {
        get => (IntPtr)_ptr->buf_ptr;
        set => _ptr->buf_ptr = (byte*)value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <para>End of the data, may be less than buffer+buffer_size if the read function returned less data than requested, e.g. for streams where no more data has been received yet.</para>
    /// <see cref="AVIOContext.buf_end" />
    /// </summary>
    public IntPtr BufEnd
    {
        get => (IntPtr)_ptr->buf_end;
        set => _ptr->buf_end = (byte*)value;
    }
    
    /// <summary>
    /// <para>original type: void*</para>
    /// <para>A private pointer, passed to the read/write/seek/... functions.</para>
    /// <see cref="AVIOContext.opaque" />
    /// </summary>
    public IntPtr Opaque
    {
        get => (IntPtr)_ptr->opaque;
        set => _ptr->opaque = (void*)value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.read_packet" />
    /// </summary>
    public AVIOContext_read_packet_func ReadPacket
    {
        get => _ptr->read_packet;
        set => _ptr->read_packet = value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.write_packet" />
    /// </summary>
    public AVIOContext_write_packet_func WritePacket
    {
        get => _ptr->write_packet;
        set => _ptr->write_packet = value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.seek" />
    /// </summary>
    public AVIOContext_seek_func Seek
    {
        get => _ptr->seek;
        set => _ptr->seek = value;
    }
    
    /// <summary>
    /// <para>position in the file of the current buffer</para>
    /// <see cref="AVIOContext.pos" />
    /// </summary>
    public long Position
    {
        get => _ptr->pos;
        set => _ptr->pos = value;
    }
    
    /// <summary>
    /// <para>true if was unable to read due to error or eof</para>
    /// <see cref="AVIOContext.eof_reached" />
    /// </summary>
    public int EofReached
    {
        get => _ptr->eof_reached;
        set => _ptr->eof_reached = value;
    }
    
    /// <summary>
    /// <para>contains the error code or 0 if no error happened</para>
    /// <see cref="AVIOContext.error" />
    /// </summary>
    public int Error
    {
        get => _ptr->error;
        set => _ptr->error = value;
    }
    
    /// <summary>
    /// <para>true if open for writing</para>
    /// <see cref="AVIOContext.write_flag" />
    /// </summary>
    public int WriteFlag
    {
        get => _ptr->write_flag;
        set => _ptr->write_flag = value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.max_packet_size" />
    /// </summary>
    public int MaxPacketSize
    {
        get => _ptr->max_packet_size;
        set => _ptr->max_packet_size = value;
    }
    
    /// <summary>
    /// <para>Try to buffer at least this amount of data before flushing it.</para>
    /// <see cref="AVIOContext.min_packet_size" />
    /// </summary>
    public int MinPacketSize
    {
        get => _ptr->min_packet_size;
        set => _ptr->min_packet_size = value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.checksum" />
    /// </summary>
    public ulong Checksum
    {
        get => _ptr->checksum;
        set => _ptr->checksum = value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <see cref="AVIOContext.checksum_ptr" />
    /// </summary>
    public IntPtr ChecksumPtr
    {
        get => (IntPtr)_ptr->checksum_ptr;
        set => _ptr->checksum_ptr = (byte*)value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.update_checksum" />
    /// </summary>
    public AVIOContext_update_checksum_func UpdateChecksum
    {
        get => _ptr->update_checksum;
        set => _ptr->update_checksum = value;
    }
    
    /// <summary>
    /// <para>Pause or resume playback for network streaming protocols - e.g. MMS.</para>
    /// <see cref="AVIOContext.read_pause" />
    /// </summary>
    public AVIOContext_read_pause_func ReadPause
    {
        get => _ptr->read_pause;
        set => _ptr->read_pause = value;
    }
    
    /// <summary>
    /// <para>Seek to a given timestamp in stream with the specified stream_index. Needed for some network streaming protocols which don't support seeking to byte position.</para>
    /// <see cref="AVIOContext.read_seek" />
    /// </summary>
    public AVIOContext_read_seek_func ReadSeek
    {
        get => _ptr->read_seek;
        set => _ptr->read_seek = value;
    }
    
    /// <summary>
    /// <para>A combination of AVIO_SEEKABLE_ flags or 0 when the stream is not seekable.</para>
    /// <see cref="AVIOContext.seekable" />
    /// </summary>
    public int Seekable
    {
        get => _ptr->seekable;
        set => _ptr->seekable = value;
    }
    
    /// <summary>
    /// <para>avio_read and avio_write should if possible be satisfied directly instead of going through a buffer, and avio_seek will always call the underlying seek function directly.</para>
    /// <see cref="AVIOContext.direct" />
    /// </summary>
    public int Direct
    {
        get => _ptr->direct;
        set => _ptr->direct = value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <para>',' separated list of allowed protocols.</para>
    /// <see cref="AVIOContext.protocol_whitelist" />
    /// </summary>
    public IntPtr ProtocolWhitelist
    {
        get => (IntPtr)_ptr->protocol_whitelist;
        set => _ptr->protocol_whitelist = (byte*)value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <para>',' separated list of disallowed protocols.</para>
    /// <see cref="AVIOContext.protocol_blacklist" />
    /// </summary>
    public IntPtr ProtocolBlacklist
    {
        get => (IntPtr)_ptr->protocol_blacklist;
        set => _ptr->protocol_blacklist = (byte*)value;
    }
    
    /// <summary>
    /// <para>A callback that is used instead of write_packet.</para>
    /// <see cref="AVIOContext.write_data_type" />
    /// </summary>
    public AVIOContext_write_data_type_func WriteDataType
    {
        get => _ptr->write_data_type;
        set => _ptr->write_data_type = value;
    }
    
    /// <summary>
    /// <para>If set, don't call write_data_type separately for AVIO_DATA_MARKER_BOUNDARY_POINT, but ignore them and treat them as AVIO_DATA_MARKER_UNKNOWN (to avoid needlessly small chunks of data returned from the callback).</para>
    /// <see cref="AVIOContext.ignore_boundary_point" />
    /// </summary>
    public int IgnoreBoundaryPoint
    {
        get => _ptr->ignore_boundary_point;
        set => _ptr->ignore_boundary_point = value;
    }
    
    /// <summary>
    /// <see cref="AVIOContext.written" />
    /// </summary>
    [Obsolete("field utilized privately by libavformat. For a public statistic of how many bytes were written out, see AVIOContext::bytes_written.")]
    public long Written
    {
        get => _ptr->written;
        set => _ptr->written = value;
    }
    
    /// <summary>
    /// <para>original type: byte*</para>
    /// <para>Maximum reached position before a backward seek in the write buffer, used keeping track of already written data for a later flush.</para>
    /// <see cref="AVIOContext.buf_ptr_max" />
    /// </summary>
    public IntPtr BufPtrMax
    {
        get => (IntPtr)_ptr->buf_ptr_max;
        set => _ptr->buf_ptr_max = (byte*)value;
    }
    
    /// <summary>
    /// <para>Read-only statistic of bytes read for this AVIOContext.</para>
    /// <see cref="AVIOContext.bytes_read" />
    /// </summary>
    public long BytesRead
    {
        get => _ptr->bytes_read;
        set => _ptr->bytes_read = value;
    }
    
    /// <summary>
    /// <para>Read-only statistic of bytes written for this AVIOContext.</para>
    /// <see cref="AVIOContext.bytes_written" />
    /// </summary>
    public long BytesWritten
    {
        get => _ptr->bytes_written;
        set => _ptr->bytes_written = value;
    }
}
