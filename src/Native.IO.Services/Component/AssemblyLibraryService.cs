using System.Reflection;

namespace Native.IO.Services.Component;

internal class AssemblyLibraryService : INativeLibraryService
{
    private readonly Assembly _assembly;
    private readonly Version _version;

    public AssemblyLibraryService(Assembly assembly)
    {
        _assembly = assembly;
        _version = assembly.GetName().Version ?? new(1, 0);
    }

    public INativeLibraryService Register(INativeLibrary library)
    {
        library.Load(_assembly, _version);
        return this;
    }
}
