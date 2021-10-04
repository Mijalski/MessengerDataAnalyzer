using MessengerDataAnalyzer.Analyzers;
using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Loaders.Files;

var fileLoader = new ConversationFromFileLoader();
    
var conversation = await fileLoader.GetConversationAsync();

foreach (var analyzer in AnalyzerExtensions.GetAllAnalyzers())
{
    var result = analyzer.AnalyzeData(conversation);
    Console.WriteLine(result);

    Console.WriteLine("======================\n\n");
}
