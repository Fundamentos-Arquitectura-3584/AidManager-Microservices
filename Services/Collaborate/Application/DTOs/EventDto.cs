using System;

namespace AidManager.Collaborate.Application.DTOs;

public record EventDto(
    int Id,
    string Name,
    DateTime EventDate,
    string Location,
    string Description,
    string Color,
    int ProjectId
);
