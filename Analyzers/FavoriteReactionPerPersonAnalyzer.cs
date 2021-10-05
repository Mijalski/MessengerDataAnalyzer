using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class FavoriteReactionPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content) && m.Reactions != null)
                .SelectMany(m => m.Reactions)
                .GroupBy(r => new { r.Actor, r.Reaction})
                .Select(r => new
                {
                    SenderName = r.Key.Actor,
                    r.Key.Reaction,
                    ReactionCount = r.Count()
                })
                .OrderByDescending(m => m.ReactionCount)
                .DistinctBy(r => r.SenderName)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.Reaction};{x.ReactionCount}"));
    }
}