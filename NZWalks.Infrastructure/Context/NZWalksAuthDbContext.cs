using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.Domain.Entities;

namespace NZWalks.Infrastructure.Context
{
    public class NZWalksAuthDbContext: IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var readerRoleId = "eefbe819-f9c5-488c-9791-1b991eb543db";
            var writerRoleId = "dda93818-f892-4c9c-b2fa-s9f890c01d14";

            // Seed data for Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName= "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName= "Writer".ToUpper()
                }
            };
            // Seed difficulties to the database
            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
