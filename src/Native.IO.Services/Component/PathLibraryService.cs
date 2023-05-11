namespace Native.IO.Services.Component;

internal class PathLibraryService : INativeLibraryService
{
    private readonly string _path;
    public PathLibraryService(string path)
    {
        _path = path;
    }

    public INativeLibraryService Register(INativeLibrary library, out bool isLoadSuccess)
    {
        library.Load(_path, out isLoadSuccess);
        return this;
    }
}
