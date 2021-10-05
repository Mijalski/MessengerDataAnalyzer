using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class ReactionsGivenToOthersPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content) && m.Reactions != null)
                .SelectMany(m => m.Reactions.Where(r => r.Actor != m.SenderName))
                .GroupBy(r => r.Actor)
                .Select(r => new
                {
                    SenderName = r.Key,
                    ReactionCount = r.Count()
                })
                .OrderByDescending(m => m.ReactionCount)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.ReactionCount}"));
    }
}