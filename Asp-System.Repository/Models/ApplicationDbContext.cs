using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_System.Repository.Models
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Artworks> tbl_Artwork { get; set; }
        public DbSet<Orders> tbl_Order { get; set; }
        public DbSet<ArtworkHasCategory> tbl_ArtworkHasCategory { get; set; }
        public DbSet<Likes> tbl_Like { get; set; }
        public DbSet<ArtworkImage> tbl_Artwork_Image { get; set; }
        public DbSet<UserNofitication> tbl_UserNofitication { get; set; }
        public DbSet<Categorys> tbl_Category { get; set; }
        public DbSet<Follower> tbl_Follower { get; set; }
        public DbSet<Packages> tbl_Package { get; set; }
        public DbSet<Poster> tbl_Poster { get; set; }
        public DbSet<Notification> tbl_Notification { get; set; }

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
