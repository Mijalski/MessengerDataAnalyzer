using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class DeletedMessagesCountPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;

    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => m.IsUnsent)
                .GroupBy(m => m.SenderName)
                .Select(m => new
                {
                    SenderName = m.Key,
                    MessageCount = m.Count()
                })
                .OrderByDescending(m => m.MessageCount)
                .ToList();

        return $"{Environment.NewLine}Analyzer {GetType().Name}{Environment.NewLine}" 
               + string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.MessageCount}"));
    }
}