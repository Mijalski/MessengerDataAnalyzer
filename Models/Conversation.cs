using System.Text.Json.Serialization;

namespace MessengerDataAnalyzer.Models;

public class Conversation
{
    public Conversation()
    {
        Participants = new List<Participant>();
        Messages = new List<Message>();
    }

    public IEnumerable<Participant> Participants { get; set; }
    public IEnumerable<Message> Messages { get; set; }

    public void AddMessageRange(IEnumerable<Message> messages)
    {
        Messages = Messages.Concat(messages);
    }
    public void SetParticipants(IEnumerable<Participant> participants)
    {
        Participants = participants;
    }
}