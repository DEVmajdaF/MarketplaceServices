using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceServices.Data;
using MarketplaceServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceServices.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<Photos> Photos { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<ChatMessages> Messages { get; set; }
        public DbSet<ChatRooms> chatrooms { get; set; }
        public DbSet<RoomUser> roomUsers { get; set; }
        public DbSet<Experience> Experiences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomUser>()
               .HasKey(x => new { x.roomsId, x.UsersId });


        }

    }

    
}
