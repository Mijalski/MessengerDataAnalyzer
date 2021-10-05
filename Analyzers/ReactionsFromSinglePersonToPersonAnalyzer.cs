using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class ReactionsFromSinglePersonToPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;

    public string AnalyzeData(Conversation conversation)
    {
        var returnString = string.Empty;
        foreach (var participant in conversation.Participants)
        {
            var countsForConversation =
                conversation.Messages
                    .Where(m => !m.IsUnsent && !string.IsNullOrEmpty(m.Content) && m.Reactions != null)
                    .SelectMany(m => m.Reactions, (m, r) => new { Message = m, Reaction = r })
                    .Where(mr => mr.Reaction.Actor == participant.Name)
                    .GroupBy(mr => mr.Message.SenderName)
                    .Select(r => new
                    {
                        SenderName = r.Key,
                        ReactionCount = r.Count()
                    })
                    .OrderByDescending(m => m.ReactionCount)
                    .ToList();

            returnString += $"{Environment.NewLine}PersonName: {Environment.NewLine}"
                            + string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.ReactionCount}"));
        }

        return returnString;
    }
}