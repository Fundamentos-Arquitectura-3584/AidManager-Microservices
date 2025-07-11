using System;

namespace AidManager.Collaborate.Domain.Entities;

public class Event
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime EventDate { get; set; }
    public required string Location { get; set; }
    public required string Description { get; set; }
    public required string Color { get; set; } // Could be a hex code or a named color
    public int ProjectId { get; set; }
    // public Project Project { get; set; } // Assuming Project entity exists and will be linked
}
