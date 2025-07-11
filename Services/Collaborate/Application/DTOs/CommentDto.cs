using System;

namespace AidManager.Collaborate.Application.DTOs;

public record CommentDto(
    int Id,
    string Content,
    int UserId,
    string UserName,    // To be populated from User service or denormalized
    string UserEmail,   // To be populated from User service or denormalized
    string UserImage,   // To be populated from User service or denormalized
    int PostId,
    DateTime CreatedAt
);
