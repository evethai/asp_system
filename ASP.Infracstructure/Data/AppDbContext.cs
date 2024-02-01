using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Asp_System.Core.Models;

namespace ASP.Infracstructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Artwork>Artwork { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ArtworkHasCategory> ArtworkHasCategory { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<ArtworkImage> Artwork_Image { get; set; }
        public DbSet<UserNofitication> UserNofitication { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Follower> Follower { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Poster> Poster { get; set; }
        public DbSet<Notification> Notification { get; set; }

        private class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.Property(u => u.FirstName);
                builder.Property(u => u.LastName);
                builder.Property(u => u.Birthday);
                builder.Property(u => u.Gender);
                builder.Property(u => u.Status);
            }
        }
    }
}
