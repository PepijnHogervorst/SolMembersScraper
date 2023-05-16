namespace Application.Services;
/// <summary>
/// Service to find XML files in given folders
/// </summary>
public interface IXmlFileFinder
{
    string[] Find(string path);
    string[] FindAtExecutableLocation();
}
