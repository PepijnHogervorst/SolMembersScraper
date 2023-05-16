using Application.Models;
using Application.Services;
using System.Xml;

namespace Infrastructure.Services;
internal class SolMembersXmlReader : ISolMembersXmlReader
{
    #region Private fields
    private XmlDocument _xml = null!;
    #endregion


    #region Public properties
    #endregion


    #region Constructor
    public SolMembersXmlReader()
    {

    }
    #endregion


    #region Public methods
    public List<SolMember> Read(string path)
    {
        if (!TryReadInXml(path)) return new();
        return ConvertXmlElementsToMembers();
    }
    #endregion


    #region Private methods
    private bool TryReadInXml(string path)
    {
        if (!File.Exists(path)) return false;
        _xml = new();
        _xml.Load(path);
        return true;
    }

    private List<SolMember> ConvertXmlElementsToMembers()
    {
        List<SolMember> members = new List<SolMember>();
        XmlNode? tableNode = _xml.SelectSingleNode("Table");
        foreach (XmlNode node in _xml)
        {

        }

        return members;
    }
    #endregion
}
