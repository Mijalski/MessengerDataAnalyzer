namespace MessengerDataAnalyzer;

public class ProgramOptions 
{
    public ProgramOptions()
    {
    }
    public ProgramOptions(string fileRootDirectory)
    {
        FileRootDirectory = fileRootDirectory;
    }

    public string FileRootDirectory { get; } = Directory.GetCurrentDirectory();
}