﻿using MessengerDataAnalyzer;
using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Loaders.Files;
using MessengerDataAnalyzer.Savers;

var options = args.Any() && !string.IsNullOrEmpty(args[0]) 
    ? new ProgramOptions(args[0]) // assign the path from argument
    : new ProgramOptions(); // get default path

var fileLoader = new ConversationFromFileLoader(options);
var fileSaver = new FileSaver(options);

fileSaver.PrepareFileForSaving();
var conversation = await fileLoader.GetConversationAsync();

foreach (var analyzer in AnalyzerExtensions.GetAllAnalyzers().Where(a => !a.IsDisabled))
{
    var result = analyzer.AnalyzeData(conversation);
    var resultWithTitle = $"{Environment.NewLine}Analyzer {analyzer.GetType().Name}{Environment.NewLine}" + result;

    Console.WriteLine(resultWithTitle);
    await fileSaver.SaveTextToFileAsync(resultWithTitle);
}

