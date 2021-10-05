using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class MessageWithWordCountPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    private static readonly List<string> WordsToFind = new ()
    {
        "kekw", "kurwa", "kekew", "garnuch", "braciak", "smalec"
    };

    public string AnalyzeData(Conversation conversation)
    {
        var returnString = string.Empty;
        foreach (var wordToFind in WordsToFind)
        {
            returnString += $"{Environment.NewLine}Analyzer {GetType().Name} for word: {wordToFind}{Environment.NewLine}";

            var countsForConversation =
                conversation.Messages
                    .Where(m => !m.IsUnsent && m.Content != null && m.Content.ToLower().Contains(wordToFind))
                    .GroupBy(m => m.SenderName)
                    .Select(m => new
                    {
                        SenderName = m.Key,
                        MessageCount = m.Count()
                    })
                    .OrderByDescending(m => m.MessageCount)
                    .ToList();

            returnString += string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.MessageCount}"));
        }

        return returnString;
    }
}