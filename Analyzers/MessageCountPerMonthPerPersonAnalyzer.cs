using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class MessageCountPerMonthPerPersonAnalyzer : IAnalyzer
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
                    DateTimeOffset.FromUnixTimeMilliseconds(m.TimestampMs).Month
                })
                .GroupBy(m => new { m.SenderName, m.Month })
                .Select(m => new
                {
                    m.Key.SenderName,
                    m.Key.Month,
                    MessageCount = m.Count()
                })
                .OrderByDescending(m => m.Month)
                .ThenByDescending(m => m.SenderName)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.Month};{x.SenderName};{x.MessageCount}"));
    }
}