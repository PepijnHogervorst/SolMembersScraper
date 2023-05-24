using Application.Models;
using Application.Services;
using System.Reflection;
using System.Xml;

namespace Infrastructure.Services;
internal class SolMembersXmlReader : ISolMembersXmlReader
{
    #region Private fields
    private XmlDocument _xml = null!;
    private List<string> _headers = new();
    private PropertyInfo[] _memberProperties = null!;
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
        using FileStream stream = new(path, FileMode.Open);
        XmlTextReader xmlReader = new(stream)
        {
            Namespaces = false
        };

        _xml = new();
        _xml.Load(xmlReader);
        return true;
    }

    private List<SolMember> ConvertXmlElementsToMembers()
    {
        List<SolMember> members = new();

        XmlElement? workbook = _xml.DocumentElement;
        if (workbook == null) return members;

        XmlNode? worksheet = workbook.SelectSingleNode("Worksheet");
        if (worksheet == null) return members;

        XmlNode? table = worksheet.SelectSingleNode("Table");
        if (table == null) return members;

        XmlNodeList? rows = table.SelectNodes("Row");
        if (rows == null) return members;
        List<XmlNode> rowNodes = rows.Cast<XmlNode>().ToList();

        GetHeaders(rowNodes.FirstOrDefault());
        DetermineMemberPropertyOrder();
        foreach (XmlNode row in rowNodes.Skip(1))
        {
            SolMember? member = ConvertXmlToMember(row);
            if (member is null) continue;
            members.Add(member);
        }

        return members;
    }

    private void GetHeaders(XmlNode? headerNode)
    {
        if (headerNode is null) return;

        XmlNodeList? cells = headerNode.SelectNodes("Cell");
        if (cells == null) return;

        _headers.Clear();

        foreach (XmlNode cell in cells)
        {
            XmlNode? data = cell.SelectSingleNode("Data");
            if (data is null) continue;
            _headers.Add(data.InnerText);
        }
    }

    /// <summary>
    /// Use reflection to get properties of class (use order or found properties!)
    /// </summary>
    private void DetermineMemberPropertyOrder()
    {
        _memberProperties = typeof(SolMember).GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    private SolMember? ConvertXmlToMember(XmlNode row)
    {
        XmlNode[]? cells = row.SelectNodes("Cell")?.Cast<XmlNode>().ToArray();
        if (cells == null) return null;
        if (cells.Length != _headers.Count) return null;

        SolMember member = new();

        for (int i = 0; i < _headers.Count; i++)
        {
            XmlNode? data = cells[i].SelectSingleNode("Data");
            if (data is null) continue;

            if (!_memberProperties[i].CanWrite) continue;
            _memberProperties[i].SetValue(member, data.InnerText);
        }

        return member;
    }
    #endregion
}
