namespace MessengerDataAnalyzer.Models;

public class Message
{
    public string SenderName { get; set; } = default!;
    public long TimestampMs { get; set; }
    public string? Content { get; set; }
    public string Type { get; set; } = default!;
    public IEnumerable<Photo>? Photos {  get; set; }
    public bool IsUnsent { get; set; }
    public IEnumerable<MessageReaction>? Reactions { get; set; }
    public SharedContent Share { get; set; }
}