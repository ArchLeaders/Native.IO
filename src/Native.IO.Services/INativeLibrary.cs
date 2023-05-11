using System.Reflection;

namespace Native.IO.Services;

public interface INativeLibrary
{
    /// <summary>
    /// Loads the <see cref="INativeLibrary"/> and saves a copy matching the <paramref name="assembly"/> and <paramref name="version"/>
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="version"></param>
    public void Load(Assembly assembly, Version version, out bool isLoadSuccess);

    /// <summary>
    /// Loads the <see cref="INativeLibrary"/> and saves it to the <paramref name="path"/>
    /// </summary>
    /// <param name="path">The absolue directory path to save the <see cref="INativeLibrary"/> to</param>
    public void Load(string path, out bool isLoadSuccess);
}
