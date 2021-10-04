using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers.Generics;

public interface IAnalyzer
{
    string AnalyzeData(Conversation conversation);
}