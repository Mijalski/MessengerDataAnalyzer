namespace MessengerDataAnalyzer.Models;

public class Message
{
    public string SenderName { get; set; } = default!;
    public double TimestampMs { get; set; }
    public string? Content { get; set; }
    public string Type { get; set; } = default!;
    public bool IsUnsent { get; set; }
    public IEnumerable<MessageReaction>? Reactions { get; set; }
    public SharedContent Share { get; set; }
}