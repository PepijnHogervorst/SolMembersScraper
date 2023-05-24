using Application.Services;

namespace Console.Services.SolScraper;
internal class SolScraperProgram : ISolScraperProgram
{
    #region Private fields
    private readonly IXmlFileFinder _fileFinder;
    private readonly ISolMembersXmlReader _reader;
    private readonly ISolMembersBeautifier _beautifier;
    #endregion


    #region Constructor
    public SolScraperProgram(IXmlFileFinder fileFinder, ISolMembersXmlReader reader,
                             ISolMembersBeautifier beautifier)
    {
        _fileFinder = fileFinder;
        _reader = reader;
        _beautifier = beautifier;
    }
    #endregion


    #region Public methods
    public void Run()
    {
        string[] files = _fileFinder.FindAtExecutableLocation();
        ProcessXmlFilesOneByOne(files);
    }
    #endregion


    #region Private methods
    private void ProcessXmlFilesOneByOne(string[] files)
    {
        foreach (string file in files)
        {
            List<Application.Models.SolMember> solMembers = _reader.Read(file);

            System.Console.WriteLine($"Read in file: {Path.GetFileName(file)}");
            foreach (Application.Models.SolMember member in solMembers)
            {
                System.Console.WriteLine($"\t{member.Id} - {member.FirstName:20} {member.LastName:20} - {member.TelephoneNumber:10}");
            }

            System.Console.WriteLine($"----- BEAUTIFYING!!! -----");
            _beautifier.Beautify(solMembers);

            foreach (Application.Models.SolMember member in solMembers)
            {
                System.Console.WriteLine($"\t{member.Id} - {member.FirstName:20} {member.LastName:20} - {member.TelephoneNumber:10}");
            }
        }
    }
    #endregion
}
