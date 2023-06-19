
using Blogger_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger_Data
{
    public class BloggerDbContext : IdentityDbContext
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserManager<IdentityRole> _roleManager;
        public BloggerDbContext(DbContextOptions<BloggerDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post_Category> Post_Categories { get; set; }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post_Category>(entity =>
            {
                entity.HasKey(e => new { e.PostID, e.CategoryID });
            });

            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if(tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            // Tạo phân quyền admin và user
            //var adminRoleId = "25d9875c-878d-414e-8e6f-b4c28815f739";
            //var userRoleId = "3195156e-ef20-4c3d-9406-7bc7e87fd6f6";
            //var roles = new List<IdentityRole>
            //{
            //    new IdentityRole
            //    {
            //        Id = adminRoleId,
            //        ConcurrencyStamp = adminRoleId,
            //        Name = "Admin",
            //        NormalizedName = "Admin".ToUpper(),
            //    },
            //    new IdentityRole
            //     {
            //        Id = userRoleId,
            //        ConcurrencyStamp = userRoleId,
            //        Name = "User",
            //        NormalizedName = "User".ToUpper()
            //     }

            //};

            //modelBuilder.Entity<IdentityRole>().HasData(roles);

            

            
        }
    }
}
