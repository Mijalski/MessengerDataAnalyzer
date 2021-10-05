using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class MessageResponseCustomCountPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    private const string WordToFind = "debil";
    public string AnalyzeData(Conversation conversation)
    {
        var messagesWithIndexes =
            conversation.Messages
                .Select((m, idx) => new
                {
                    m.SenderName,
                    m.Content,
                    m.IsUnsent,
                    Index = idx
                })
                .ToList();

        var messagesResponseIndexes =
                messagesWithIndexes
                .Where(m => !m.IsUnsent
                            && !string.IsNullOrEmpty(m.Content)
                            && m.Content.ToLower().StartsWith(WordToFind))
                .Select(m => m.Index - 1);

        var countsForConversation =
            messagesResponseIndexes
                .Select(idx => new
                {
                    Message = messagesWithIndexes[idx]
                })
                .GroupBy(m => m.Message.SenderName)
                .Select(m => new
                {
                    SenderName = m.Key,
                    MessageCount = m.Count()
                })
                .OrderByDescending(m => m.MessageCount)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.MessageCount}"));
    }
}