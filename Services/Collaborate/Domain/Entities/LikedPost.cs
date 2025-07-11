namespace AidManager.Collaborate.Domain.Entities;

public class LikedPost
{
    public int UserId { get; set; }
    // public User User { get; set; } // Assuming User entity exists

    public int PostId { get; set; }
    // public Post Post { get; set; } // Assuming Post entity exists

    // Consider adding a DateTime LikedAt if needed
}
