using System.Text;

namespace MessengerDataAnalyzer.Savers;

public class FileSaver
{
    private readonly string _resultFileDirectory;
    private readonly string _resultFilePath;

    public FileSaver(ProgramOptions options)
    {
        _resultFileDirectory = Path.Combine(options.FileRootDirectory, "output");
        _resultFilePath = Path.Combine(_resultFileDirectory, Path.GetFileName("result.txt"));
    }


    public void PrepareFileForSaving()
    {
        if (!Directory.Exists(_resultFileDirectory))
        {
            Directory.CreateDirectory(_resultFileDirectory);
        }
        
        if (File.Exists(_resultFilePath))
        {
            File.Delete(_resultFilePath);
        }
    }

    public async Task SaveTextToFileAsync(string text)
    {
        await File.AppendAllTextAsync(_resultFilePath, text);
    }
}