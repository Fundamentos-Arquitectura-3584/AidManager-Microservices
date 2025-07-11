using System;
using System.Collections.Generic;

namespace AidManager.Collaborate.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Subject { get; set; }
    public required string Description { get; set; }
    public List<string> ImageUrls { get; set; }
    public List<Comment> Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Rating { get; set; } // Could be sum of likes or a separate rating system
    public int CompanyId { get; set; }
    // public Company Company { get; set; } // Assuming Company entity exists
    public int UserId { get; set; }
    // public User User { get; set; } // Assuming User entity exists

    public Post()
    {
        ImageUrls = new List<string>();
        Comments = new List<Comment>();
        CreatedAt = DateTime.UtcNow;
        Rating = 0;
    }
}
