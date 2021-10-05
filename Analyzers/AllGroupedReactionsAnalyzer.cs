using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class AllGroupedReactionsAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content) && m.Reactions != null)
                .SelectMany(m => m.Reactions)
                .GroupBy(r => r.Reaction)
                .Select(r => new
                {
                    Reaction = r.Key,
                    ReactionCount = r.Count()
                })
                .OrderByDescending(r => r.ReactionCount)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.Reaction};{x.ReactionCount}"));
    }
}