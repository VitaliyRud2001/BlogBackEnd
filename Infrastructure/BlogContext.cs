using Infrastructure.Models;
using Infrastructure.Models.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTag { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id=1,TagName="Comedy"},
                new Tag { Id=2,TagName="science"},
                new Tag { Id=3,TagName="Films"}
                );
            modelBuilder.Entity<User>().HasData(
                new User { Id=1,Email="vitaliy2001rudenko",FirstName="vitaliy",LastName="Rudenko",Password="usedrugs3"}
                );
        }
    }
}
