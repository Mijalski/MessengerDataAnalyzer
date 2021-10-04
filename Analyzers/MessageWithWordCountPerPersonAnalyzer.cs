using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class MessageWithWordCountPerPersonAnalyzer : IAnalyzer
{
    private static List<string> _wordsToFind = new ()
    {
        "kekw", "kurwa"
    };

    public string AnalyzeData(Conversation conversation)
    {
        var returnString = string.Empty;
        foreach (var wordToFind in _wordsToFind)
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

            returnString += string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName}: {x.MessageCount} messages"));
        }

        return returnString;
    }
}