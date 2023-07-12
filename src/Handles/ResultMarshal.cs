using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace Native.IO.Handles;

public enum Result
{
    Ok,
    Bad
}

public unsafe partial class ResultMarshal : SafeHandleZeroOrMinusOneIsInvalid
{
    [LibraryImport("native_io")]
    private static partial void GetExceptionHandle(IntPtr handle, out byte* dst);

    [LibraryImport("native_io")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool FreeExceptionHandle(IntPtr handle);

    private const string DefaultResult = "OK";

    public ResultMarshal() : base(true) { }

    public static implicit operator bool(ResultMarshal result) => result.IsInvalid;
    public static implicit operator Result(ResultMarshal result) => result.Result;
    public static implicit operator string(ResultMarshal result) => result.ToString();

    public Result Result {
        get => IsInvalid ? Result.Ok : Result.Bad;
    }

    public override string ToString()
    {
        if (handle > IntPtr.Zero) {
            GetExceptionHandle(handle, out byte* dst);
            return Utf8StringMarshaller.ConvertToManaged(dst)
                ?? DefaultResult;
        }

        return DefaultResult;
    }

    protected override bool ReleaseHandle()
    {
        if (handle > IntPtr.Zero) {
            FreeExceptionHandle(handle);
        }

        return true;
    }
}
