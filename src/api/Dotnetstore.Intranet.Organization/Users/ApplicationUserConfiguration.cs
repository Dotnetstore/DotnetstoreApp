using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var converter = new ValueConverter<ApplicationUserId, Guid>(
            v => v.Value,
            v => ApplicationUserId.Create(v));
        
        builder
            .ToTable(nameof(ApplicationUser).ToLower());
        
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasIndex(x => x.Id)
            .IsUnique();
        
        builder
            .HasIndex(x => x.EmailAddress)
            .IsUnique();
        
        builder
            .Property(x => x.Id)
            .HasConversion(converter)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(DataSchemeConstants.UserLastNameMaxLength)
            .IsUnicode(false);
        
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(DataSchemeConstants.UserFirstNameMaxLength)
            .IsUnicode(false);
        
        builder.Property(p => p.MiddleName)
            .HasMaxLength(DataSchemeConstants.UserMiddleNameMaxLength)
            .IsUnicode(false);
        
        builder.Property(p => p.DateOfBirth)
            .IsRequired();
        
        builder.Property(p => p.IsMale)
            .IsRequired();
        
        builder.Property(p => p.SocialSecurityNumber)
            .IsRequired(false)
            .HasMaxLength(DataSchemeConstants.UserSocialSecurityNumberMaxLength)
            .IsUnicode(false);
        
        builder
            .Property(x => x.EmailAddress)
            .IsRequired()
            .HasMaxLength(DataSchemeConstants.UserEmailMaxLength)
            .IsUnicode(false);
        
        builder
            .Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(DataSchemeConstants.UserPasswordMaxLength)
            .IsUnicode(false);

        builder
            .Property(x => x.EmailAddressConfirmationCode)
            .IsRequired(false)
            .HasMaxLength(DataSchemeConstants.UserEmailConfirmationCodeMaxLength)
            .IsUnicode(false);
    }
}