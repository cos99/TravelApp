using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserAdd> Added { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAdd>().HasKey(k => new { k.SourceUserId, k.AddedUserId });

            builder.Entity<UserAdd>().HasOne(s => s.SourceUser)
                                     .WithMany(l => l.AddedUsers)
                                     .HasForeignKey(s => s.SourceUserId)
                                     .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserAdd>().HasOne(s => s.AddedUser)
                                     .WithMany(l => l.AddedByUsers)
                                     .HasForeignKey(s => s.AddedUserId)
                                     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}