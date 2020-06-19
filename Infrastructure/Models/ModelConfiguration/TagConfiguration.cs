using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models.ModelConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.TagName)
                .IsRequired()
                .HasColumnName("tagTitle")
                .HasMaxLength(40);
        }
    }
}
