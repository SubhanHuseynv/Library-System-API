using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Persistence.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name).HasColumnType("nvarchar(256)");
            builder.Property(b => b.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasIndex(b => b.Name).IsUnique();
        }
    }
}
