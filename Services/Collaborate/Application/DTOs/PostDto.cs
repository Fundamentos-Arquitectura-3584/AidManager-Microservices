using System;
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.DTOs;

public record PostDto(
    int Id,
    string Title,
    string Subject,
    string Description,
    DateTime CreatedAt,
    int CompanyId,
    int UserId,
    string UserName,    // To be populated from User service or denormalized
    string UserImage,   // To be populated from User service or denormalized
    string Email,       // User's email, to be populated
    int Rating,
    List<string> ImageUrls,
    List<CommentDto> Comments
);
