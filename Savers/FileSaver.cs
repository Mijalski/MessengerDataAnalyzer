using System.Text;

namespace MessengerDataAnalyzer.Savers;

public class FileSaver
{
    private readonly ProgramOptions _options;

    public FileSaver(ProgramOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }


    public void PrepareFileForSaving()
    {
        if (File.Exists(_options.OutputFilePath))
        {
            File.Delete(_options.OutputFilePath);
        }
    }

    public async Task SaveTextToFileAsync(string text)
    {
        await File.AppendAllTextAsync(_options.OutputFilePath, text);
    }
}