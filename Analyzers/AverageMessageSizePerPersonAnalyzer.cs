using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class AverageMessageSizePerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content))
                .Select(m => new
                {
                    m.SenderName,
                    MessageSize = m.Content.Length
                })
                .GroupBy(m => m.SenderName)
                .Select(m => new
                {
                    SenderName = m.Key,
                    AverageMessageSize = m.Sum(x => x.MessageSize) / m.Count()
                })
                .OrderByDescending(m => m.AverageMessageSize)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.AverageMessageSize}"));
    }
}