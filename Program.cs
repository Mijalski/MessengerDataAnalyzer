using MessengerDataAnalyzer;
using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Loaders.Files;
using MessengerDataAnalyzer.Savers;

var options = new ProgramOptions();
var fileLoader = new ConversationFromFileLoader(options);
var fileSaver = new FileSaver(options);

fileSaver.PrepareFileForSaving();
var conversation = await fileLoader.GetConversationAsync();

foreach (var analyzer in AnalyzerExtensions.GetAllAnalyzers().Where(a => !a.IsDisabled))
{
    var result = analyzer.AnalyzeData(conversation);
    Console.WriteLine($"{Environment.NewLine}Analyzer {analyzer.GetType().Name}{Environment.NewLine}" + result);
    await fileSaver.SaveTextToFileAsync($"{Environment.NewLine}Analyzer {analyzer.GetType().Name}{Environment.NewLine}" + result);
}
