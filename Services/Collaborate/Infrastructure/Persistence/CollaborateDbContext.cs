using Microsoft.EntityFrameworkCore;
using AidManager.Collaborate.Domain.Entities;
using System.Reflection;

namespace AidManager.Collaborate.Infrastructure.Persistence;

public class CollaborateDbContext : DbContext
{
    public CollaborateDbContext(DbContextOptions<CollaborateDbContext> options) : base(options)
    {
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<FavoritePost> FavoritePosts { get; set; }
    public DbSet<LikedPost> LikedPosts { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations from the current assembly
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // This line is more common if you have separate IEntityTypeConfiguration<T> classes.
        // For direct configuration or simple cases, you can define them here.

        // Example: Composite primary key for FavoritePost
        modelBuilder.Entity<FavoritePost>()
            .HasKey(fp => new { fp.UserId, fp.PostId });

        // Example: Composite primary key for LikedPost
        modelBuilder.Entity<LikedPost>()
            .HasKey(lp => new { lp.UserId, lp.PostId });

        // Relationships (examples, assuming User and Company are not part of this DbContext directly)
        // If they were, you would define foreign keys like:
        // modelBuilder.Entity<Post>()
        //     .HasOne<User>() // If User entity was in Collaborate.Domain
        //     .WithMany()
        //     .HasForeignKey(p => p.UserId)
        //     .OnDelete(DeleteBehavior.Restrict); // Or Cascade, SetNull as appropriate

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne() // Assuming Comment has a PostId but not a navigation property back to Post directly in its entity structure for simplicity
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a post also deletes its comments

        modelBuilder.Entity<Post>()
            .Property(p => p.ImageUrls)
            .HasConversion(
                v => string.Join(',', v), // Convert List<string> to comma-separated string
                v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList() // Convert string back to List<string>
            );

        // Add further configurations for other entities and relationships as needed.
        // For example, Project-Event relationship:
        modelBuilder.Entity<Project>()
            .HasMany<Event>() // Assuming Event has a ProjectId but no direct navigation property to Project for simplicity
            // .WithOne(e => e.Project) // If Event had a Project navigation property
            .WithOne()
            .HasForeignKey("ProjectId") // Assuming Event.ProjectId is the FK
            .OnDelete(DeleteBehavior.Cascade); // Deleting a project also deletes its events

        // Note: The handling of User and Company entities (which seem to be external or from another context like IAM)
        // needs careful consideration. If they are truly external, this DbContext would only store their IDs.
        // If you need to query them or include their data, you might use a separate DbContext for IAM
        // and perform joins at a higher level or use service calls.
    }
}
