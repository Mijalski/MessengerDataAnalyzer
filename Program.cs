using MessengerDataAnalyzer.Analyzers;
using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Loaders.Files;
using MessengerDataAnalyzer.Savers;

var fileLoader = new ConversationFromFileLoader();
var fileSaver = new FileSaver();

fileSaver.PrepareFileForSaving();
var conversation = await fileLoader.GetConversationAsync();

foreach (var analyzer in AnalyzerExtensions.GetAllAnalyzers().Where(a => !a.IsDisabled))
{
    var result = analyzer.AnalyzeData(conversation);
    Console.WriteLine(result);
    await fileSaver.SaveTextToFileAsync(result);
}
