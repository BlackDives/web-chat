namespace web_api.Dtos.DirectMessages;

public class DirectMessageChannelDTO
{
    public string Id { get; set; }
    public string UserOneId { get; set; }
    public string UserOneUsername { get; set; }
    public string UserTwoId { get; set; }
    public string UserTwoUsername { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}