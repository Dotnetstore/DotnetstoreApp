using DotnetstoreApp.Organization.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetstoreApp.Organization.Configurations;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(25)
            .IsUnicode();
        
        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(25)
            .IsUnicode();
        
        builder
            .Property(x => x.MiddleName)
            .HasMaxLength(25)
            .IsUnicode();
        
        builder
            .Property(x => x.DateOfBirth)
            .IsRequired();
        
        builder
            .Property(x => x.IsMale)
            .IsRequired()
            .HasDefaultValue(true);
    }
}