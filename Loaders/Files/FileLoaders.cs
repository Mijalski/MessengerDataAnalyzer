using System.Text;
using System.Text.Json;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Loaders.Files;

public class ConversationFromFileLoader : IConversationLoader
{
    private readonly ProgramOptions _options;

    public ConversationFromFileLoader(ProgramOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Conversation> GetConversationAsync()
    {
        var fileEntries = Directory.GetFiles(_options.FilesRootPath);
        var conversation = new Conversation();

        foreach (var file in fileEntries)
        {
            var json = await File.ReadAllTextAsync(file);
            var conversationPart = DecodeString(json).DeserializeUsingDefaultSettings<Conversation>();

            if (conversationPart is null)
            {
                throw new Exception($"Could not parse JSON file: {file}");
            }

            conversation.AddMessageRange(conversationPart.Messages);

            if (!conversation.Participants.Any())
            {
                conversation.SetParticipants(conversationPart.Participants);
            }
        }

        return conversation;
    }
    private static string DecodeString(string text)
    {
        text = text.Replace(@"\\", @"\\\\").Replace(@"\n", @"\\n").Replace(@"\""", @"\\"""); // Facebook sucks!
        var targetEncoding = Encoding.GetEncoding("ISO-8859-1");
        var unescapedText = System.Text.RegularExpressions.Regex.Unescape(text);
        return Encoding.UTF8.GetString(targetEncoding.GetBytes(unescapedText));
    }
}