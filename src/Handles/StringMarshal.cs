using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;

namespace Native.IO.Handles;

/// <summary>
/// Marshals a C++ <b>std::string</b> pointer and provides an API to
/// interface it without copying the data
/// </summary>
public unsafe partial class StringMarshal : SafeHandleZeroOrMinusOneIsInvalid
{
    [LibraryImport("native_io")]
    private static partial void GetStringHandle(IntPtr handle, out byte* ptr, out int len);

    [LibraryImport("native_io")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool FreeStringHandle(IntPtr handle);

    public StringMarshal() : base(true) { }

    private byte* _ptr;
    private int _len;

    public static implicit operator Span<byte>(StringMarshal str) => str.AsSpan();
    public static implicit operator ReadOnlySpan<byte>(StringMarshal str) => str.AsSpan();

    /// <summary>
    /// Returns a floating span over the unmanaged memory range
    /// </summary>
    public Span<byte> AsSpan()
    {
        GetStringHandle(handle, out _ptr, out _len);
        return new(_ptr, _len);
    }

    /// <inheritdoc cref="ToString(Encoding)"/>
    public override string ToString() => ToString(Encoding.UTF8);

    /// <summary>
    /// Creates a managed copy of the unmanaged string pointer
    /// </summary>
    /// <param name="encoding">The encoding to use when decoding the raw data into a managed string</param>
    public string ToString(Encoding encoding)
    {
        return encoding.GetString(_ptr, _len);
    }

    protected override bool ReleaseHandle()
    {
        return FreeStringHandle(handle);
    }
}
