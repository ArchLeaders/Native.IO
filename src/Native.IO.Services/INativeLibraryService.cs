namespace Native.IO.Services;

public interface INativeLibraryService
{
    /// <summary>
    /// Registers an <see cref="INativeLibrary"/> instance with the owning <see cref="INativeLibraryService"/>
    /// </summary>
    /// <param name="library">The <see cref="INativeLibrary"/> instance to register</param>
    /// <returns><see cref="INativeLibraryService"/></returns>
    public INativeLibraryService Register(INativeLibrary library);
}
