using Application.Services;
using System.Reflection;

namespace Infrastructure.Services;
internal class XmlFileFinder : IXmlFileFinder
{
    #region Public methods
    /// <summary>
    /// Finds xml files in given directory
    /// Does not search in nested folders.
    /// </summary>
    public string[] Find(string path)
    {
        FileAttributes attribute = File.GetAttributes(path);
        string? dirPath = path;
        if (!attribute.HasFlag(FileAttributes.Directory))
        {
            dirPath = Path.GetDirectoryName(path);
            if (dirPath == null) return Array.Empty<string>();
        }

        return Directory.GetFiles(dirPath, "*.xml");
    }

    public string[] FindAtExecutableLocation()
    {
        string path = Assembly.GetExecutingAssembly().Location;
        return Find(path);
    }
    #endregion
}
