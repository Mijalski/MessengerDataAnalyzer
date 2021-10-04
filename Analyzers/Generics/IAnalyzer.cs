using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers.Generics;

public interface IAnalyzer
{
    bool IsDisabled { get; }
    string AnalyzeData(Conversation conversation);
}