using MessengerDataAnalyzer.Analyzers.Generics;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Analyzers;

public class PicturesCountPerPersonAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    public string AnalyzeData(Conversation conversation)
    {
        var countsForConversation =
            conversation.Messages
                .Where(m => m.Photos != null && m.Photos.Any())
                .Select(m => new
                {
                    PhotosCount = m.Photos.Count(),
                    m.SenderName
                })
                .GroupBy(m => m.SenderName)
                .Select(m => new
                {
                    SenderName = m.Key,
                    PhotosCount = m.Sum(p => p.PhotosCount)
                })
                .OrderByDescending(m => m.PhotosCount)
                .ToList();

        return string.Join(Environment.NewLine, countsForConversation.Select(x => $"{x.SenderName};{x.PhotosCount}"));
    }
}