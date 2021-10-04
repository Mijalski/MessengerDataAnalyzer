using MessengerDataAnalyzer.Models;

namespace MessengerDataAnalyzer.Loaders;

public interface IConversationLoader
{
    Task<Conversation> GetConversationAsync();

}