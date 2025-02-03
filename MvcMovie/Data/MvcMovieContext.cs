using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : IdentityDbContext<ApplicationUser>
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
            public DbSet<User> Users { get; set; } // Include the User entity
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optionally, configure the User entity further
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id); // Ensure the primary key is defined
            entity.Property(u => u.GoogleId).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.Name).IsRequired();
        });
    }


    //     protected override void OnModelCreating(ModelBuilder builder)
    //     {
    //         base.OnModelCreating(builder); // Required for Identity

    //         // Additional configurations for your Movie entity (if any)
    //     }
    // }
}
}