using System.Text;
using System.Text.Json;
using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Loaders.Files;

public class ConversationFromFileLoader : IConversationLoader
{
    private const string FilesRootPath = "D:\\KYRGaming\\Facebook data\\ekipa";

    public async Task<Conversation> GetConversationAsync()
    {
        var fileEntries = Directory.GetFiles(FilesRootPath);
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