using System;

namespace AidManager.Collaborate.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public double Rating { get; set; } // Average rating
    public int CompanyId { get; set; }
    // public Company Company { get; set; } // Assuming Company entity exists (possibly from Iam context or a shared kernel)
    public DateTime ProjectDateTime { get; set; }
    public required string ProjectLocation { get; set; }
    public DateTime AuditDate { get; set; } // Last audit or review date

    public Project()
    {
        AuditDate = DateTime.UtcNow;
    }
}
