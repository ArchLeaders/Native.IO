using Native.IO.Services;

namespace Native.IO.Managed;
internal class CommonLibrary : NativeLibrary<CommonLibrary>, INativeLibrary
{
    protected override string Name { get; } = "native_io";
}
