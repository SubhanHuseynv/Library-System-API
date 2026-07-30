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
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(u => u.Surname).IsRequired().HasColumnType("nvarchar(255)");
            builder.HasIndex(u => u.IdentityCardNumber).IsUnique();
            
        }
    }
}
