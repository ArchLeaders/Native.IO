using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Native.IO.Handles;

/// <summary>
/// Marshals a C++ <b>std::vector&lt;u8&gt;</b> pointer and provides an API to interface it
/// </summary>
public unsafe partial class DataMarshal : SafeHandleZeroOrMinusOneIsInvalid
{
    [LibraryImport("native_io")]
    private static partial void GetVectorHandle(IntPtr vector, out byte* ptr, out int len);

    [LibraryImport("native_io")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool FreeVectorHandle(IntPtr handle);

    public DataMarshal() : base(true) { }

    private byte* _ptr;
    private int _len;

    public static implicit operator Span<byte>(DataMarshal data) => data.AsSpan();
    public static implicit operator ReadOnlySpan<byte>(DataMarshal data) => data.AsSpan();

    public Span<byte> AsSpan()
    {
        GetVectorHandle(handle, out _ptr, out _len);
        return new(_ptr, _len);
    }

    /// <summary>
    /// Creates a managed copy of the unmanaged data pointer
    /// </summary>
    /// <returns></returns>
    public byte[] ToArray()
    {
        return AsSpan().ToArray();
    }

    protected override bool ReleaseHandle()
    {
        return FreeVectorHandle(handle);
    }
}
