using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Persistence.Configurations;

internal class BookMemberConfiguration : IEntityTypeConfiguration<BookMember>
{
    public void Configure(EntityTypeBuilder<BookMember> builder)
    {
        builder.HasKey(bm => new { bm.BookId, bm.MemberId });
    }
}
