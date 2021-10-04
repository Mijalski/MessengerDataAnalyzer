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
            var jsonString = await File.ReadAllTextAsync(file, Encoding.UTF8);
            var conversationPart = jsonString.DeserializeUsingDefaultSettings<Conversation>();

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
}