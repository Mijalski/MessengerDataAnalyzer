using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class ReactionsReceivedPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content) && m.Reactions != null && m.Reactions.Any())
                .Select(m => new 
                {
                    MessageReactionsCount = m.Reactions.Count(r => r.Actor != m.SenderName),
                    m.SenderName
                })
                .GroupBy(m => m.SenderName)
                .Select(m => new
                {
                    SenderName = m.Key,
                    OverallReactionsCount = m.Sum(m => m.MessageReactionsCount)
                })
                .OrderByDescending(m => m.OverallReactionsCount)
                .ToList();

        return $"{Environment.NewLine}Analyzer {GetType().Name}{Environment.NewLine}" 
               + string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.OverallReactionsCount}"));
    }
}