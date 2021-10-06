using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class WordExtractorAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = true;
    public string AnalyzeData(Conversation conversation)
    {
        var messages =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content))
                .Select(m => m.Content);

        return string.Join(Environment.NewLine, messages);
    }
}