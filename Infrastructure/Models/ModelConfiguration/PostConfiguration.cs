using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.ModelConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasMaxLength(50);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.UserId);

            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()");

            


        }
    }
}
