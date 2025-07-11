using System;
using System.Collections.Generic;

namespace AidManager.Collaborate.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    // public User User { get; set; } // Assuming User entity exists and will be linked
    public required string Content { get; set; }
    public int PostId { get; set; }
    // public Post Post { get; set; } // Assuming Post entity exists and will be linked
    public DateTime CreatedAt { get; set; }

    public Comment()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
