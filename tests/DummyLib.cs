using Native.IO.Services;

namespace Native.IO.Tests;

public class DummyLib : NativeLibrary<DummyLib>, INativeLibrary
{
    protected override string Name { get; } = "Dummy";
}
