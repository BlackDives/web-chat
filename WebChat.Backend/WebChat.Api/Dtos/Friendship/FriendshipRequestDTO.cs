namespace web_api.Dtos.Friendship;

public class FriendshipRequestDTO
{
    public string RequestedName { get; set; }
    public string RequesterName { get; set; }
    public string RequesterId { get; set; }
    public string? RequestedId { get; set; }
}