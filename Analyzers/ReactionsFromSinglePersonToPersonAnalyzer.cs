using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class ReactionsFromSinglePersonToPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    private const string PersonName = "Sender Name"; // put any username here

    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content) && m.Reactions != null)
                .SelectMany(m => m.Reactions, (m, r) => new { Message = m, Reaction = r})
                .Where(mr => mr.Reaction.Actor == PersonName)
                .GroupBy(mr => mr.Message.SenderName)
                .Select(r => new
                {
                    SenderName = r.Key,
                    ReactionCount = r.Count()
                })
                .OrderByDescending(m => m.ReactionCount)
                .ToList();

        return $"PersonName{Environment.NewLine}" 
               + string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.ReactionCount}"));
    }
}