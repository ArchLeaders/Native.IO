using Native.IO.Services;
using Native.IO.Tests;

NativeLibraryManager.RegisterAssembly(typeof(Program).Assembly)
    .Register(DummyLib.Shared, out bool dummyLibLoaded);

Console.WriteLine($"Dummy Lib Loaded: {dummyLibLoaded}");