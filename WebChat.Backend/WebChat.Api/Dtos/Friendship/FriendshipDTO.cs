namespace web_api.Dtos.Friendship;

public class FriendshipDTO
{
    public string FriendOneId { get; set; }
    public string FriendOneName { get; set; }
    public string FriendTwoId { get; set; }
    public string FriendTwoName { get; set; }
    public DateTime CreatedAt { get; set; }
}