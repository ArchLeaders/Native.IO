using Native.IO.Services;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Native.IO;

public abstract class NativeLibrary<T> : INativeLibrary where T : INativeLibrary, new()
{
    private static readonly Assembly _assembly = typeof(T).Assembly;
    private static readonly string _assemblyName = _assembly.GetName().Name
        ?? throw new InvalidProgramException($"The calling assembly '{_assembly}' did not have a name");

    /// <summary>
    /// A static shared instance of the <see cref="NativeLibrary{T}"/> object
    /// </summary>
    public static T Shared { get; } = new();

    /// <summary>
    /// The name of the <see cref="INativeLibrary"/> (without a file extension)
    /// </summary>
    protected abstract string Name { get; }

    /// <summary>
    /// The embedded path to the <see cref="INativeLibrary"/> files (without a trailing dot)<br/>
    /// <i>Default: <b>.Lib</b></i>
    /// </summary>
    protected virtual string EmbeddedPath { get; } = ".Lib";

    /// <summary>
    /// This works until the assembly name matches the csproj name
    /// </summary>
    private string RealName => Name + GetLibraryExtension();

    /// <summary>
    /// Resolve the library extension per platform
    /// <summary>
    private string GetLibraryExtension()
    {
        if (OperatingSystem.IsWindows()) {
            return ".dll";
        }
        else if (OperatingSystem.IsMacOS()) {
            return ".dylib";
        }
        else  {
            return ".so";
        }
    }

    public virtual void Load(Assembly assembly, Version version, out bool isLoadSuccess)
    {
        string path = Path.Combine(Path.GetTempPath(), _assemblyName ?? "Default", assembly.GetName().Name ?? "Default", version.ToString());
        Load(path, out isLoadSuccess);
    }

    public virtual void Load(string path, out bool isLoadSuccess)
    {
        try {
            if (Name == _assemblyName) {
                throw new InvalidOperationException(
                    $"The INativeLibrary '{RealName}' could not be loaded because it shares the name of the calling assembly");
            }

            Stream stream = _assembly.GetManifestResourceStream($"{_assemblyName}{EmbeddedPath}.{RealName}")
                ?? throw new InvalidOperationException(
                    $"The INativeLibrary '{_assemblyName}{EmbeddedPath}.{RealName}' could not be found in the assembly '{_assembly}'");

            string realFilePath = Path.Combine(path, RealName);

#if RELEASE
            if (!File.Exists(realFilePath)) {
                Directory.CreateDirectory(path);
                using FileStream fs = File.Create(realFilePath);
                stream.CopyTo(fs);
            }
#elif DEBUG
            Directory.CreateDirectory(path);
            using FileStream fs = File.Create(realFilePath);
            stream.CopyTo(fs);
#endif

        }
        catch (IOException ex) {
            throw new COMException($"The INativeLibrary '{RealName}' failed to extract because it's already in use", ex);
        }
        catch (Exception ex) {
            throw new COMException($"The INativeLibrary '{RealName}' failed to load", ex);
        }
        finally {
            isLoadSuccess = NativeLibrary.TryLoad(Path.Combine(path, RealName), out _);
        }
    }
}
