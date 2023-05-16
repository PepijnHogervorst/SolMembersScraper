using Application.Models;

namespace Application.Services;
/// <summary>
/// Reads in a xml file and converts it to a list of sol members 
/// </summary>
public interface ISolMembersXmlReader
{
    List<SolMember> Read(string path);
}
