using Application.Services;

namespace Console.Services.SolScraper;
internal class SolScraperProgram : ISolScraperProgram
{
    #region Private fields
    private readonly IXmlFileFinder _fileFinder;
    private readonly ISolMembersXmlReader _reader;
    #endregion


    #region Constructor
    public SolScraperProgram(IXmlFileFinder fileFinder, ISolMembersXmlReader reader)
    {
        _fileFinder = fileFinder;
        _reader = reader;
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
            _reader.Read(file);
        }
    }
    #endregion
}
