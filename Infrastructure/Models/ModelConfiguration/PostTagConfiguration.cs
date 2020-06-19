using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.ModelConfiguration
{
    class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(e => new { e.PostId, e.TagId });

            builder
                .HasOne(p => p.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Tag)
                .WithMany(p => p.PostTag)
                .HasForeignKey(p => p.TagId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
