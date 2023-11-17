using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class WordCountPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content))
                .GroupBy(m => m.SenderName)
                .Select(m => new
                {
                    SenderName = m.Key,
                    WordCount = m.Sum(x => x.Content?.Count(c => c == ' ') + 1)
                });

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.WordCount}"));
    }
}