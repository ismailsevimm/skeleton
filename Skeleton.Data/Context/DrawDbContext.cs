using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skeleton.Models.Entities;

namespace Skeleton.Data.Context
{
    // Identity üyelik tablolar //
    public class DrawDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DrawDbContext(DbContextOptions<DrawDbContext> options) : base(options)

        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
