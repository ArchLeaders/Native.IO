using Native.IO.Services;

NativeLibraryManager.RegisterPath("D:\\Bin\\Somewhere", out bool isCommonLoaded);

Console.WriteLine($"Common Lib Loaded: {isCommonLoaded}");