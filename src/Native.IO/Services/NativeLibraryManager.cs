using Native.IO.Services.Component;
using System.Reflection;

namespace Native.IO.Services;

public class NativeLibraryManager
{
    /// <summary>
    /// Initializes a new <see cref="INativeLibraryService"/> from the provided assembly
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> used by the <see cref="INativeLibrary"/> during initialization</param>
    /// <returns><see cref="INativeLibraryService"/></returns>
    public static INativeLibraryService RegisterAssembly(Assembly assembly)
    {
        return new AssemblyLibraryService(assembly);
    }

    /// <summary>
    /// Initializes a new <see cref="INativeLibraryService"/> with the provided path
    /// </summary>
    /// <param name="path">The path used by the <see cref="INativeLibrary"/> during initialization</param>
    /// <returns><see cref="INativeLibraryService"/></returns>
    public static INativeLibraryService RegisterPath(string path)
    {
        return new PathLibraryService(path);
    }
}
