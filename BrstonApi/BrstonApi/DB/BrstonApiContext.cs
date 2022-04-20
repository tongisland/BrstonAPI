using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrstonApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrstonApi.DB
{
    public class BrstonApiContext:DbContext
    {
        public BrstonApiContext(DbContextOptions<BrstonApiContext> options)
            : base(options)
        {
        }

        public DbSet<Users> UsersItems { get; set; }

        public DbSet<UserLog> UserLogItems { get; set; }

        public DbSet<UserColumns> UserColumnsItems { get; set; }

        public DbSet<ApiVersion> ApiVersionItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("B_users");
        }
    }
}
