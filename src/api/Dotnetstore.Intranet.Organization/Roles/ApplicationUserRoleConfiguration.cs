using Dotnetstore.Intranet.SDK.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        var converter = new ValueConverter<ApplicationUserRoleId, Guid>(
            v => v.Value,
            v => ApplicationUserRoleId.Create(v));
        
        builder
            .HasKey(x => x.Id);
        
        builder
            .HasIndex(x => x.Id)
            .IsUnique();
        
        builder
            .Property(x => x.Id)
            .HasConversion(converter)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(DataSchemeConstants.UserRoleNameMaxLength)
            .IsUnicode(false);
        
        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(DataSchemeConstants.UserRoleDescriptionLength)
            .IsUnicode(false);
    }
}