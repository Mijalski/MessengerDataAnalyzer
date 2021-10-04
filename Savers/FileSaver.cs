namespace MessengerDataAnalyzer.Savers;

public class FileSaver
{
    private const string FilePath = "D:\\KYRGaming\\Facebook data\\ekipa results\\output.txt";

    public void PrepareFileForSaving()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }

    public async Task SaveTextToFileAsync(string text)
    {
        await File.AppendAllTextAsync(FilePath, text);
    }
}