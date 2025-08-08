using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal sealed class ApplicationUserInRoleConfiguration : IEntityTypeConfiguration<ApplicationUserInRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserInRole> builder)
    {
        var converter = new ValueConverter<ApplicationUserInRoleId, Guid>(
            v => v.Value,
            v => ApplicationUserInRoleId.Create(v));
        
        var userConverter = new ValueConverter<ApplicationUserId, Guid>(
            v => v.Value,
            v => ApplicationUserId.Create(v));
        
        var roleConverter = new ValueConverter<ApplicationUserRoleId, Guid>(
            v => v.Value,
            v => ApplicationUserRoleId.Create(v));
        
        builder
            .ToTable(nameof(ApplicationUserInRole).ToLower());
        
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
            .Property(x => x.ApplicationUserId)
            .HasConversion(userConverter)
            .IsRequired(); 
        
        builder
            .Property(x => x.ApplicationUserRoleId)
            .HasConversion(roleConverter)
            .IsRequired();
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.ApplicationUserInRoles)
            .HasForeignKey(x => x.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.ApplicationUserInRoles)
            .HasForeignKey(x => x.ApplicationUserRoleId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}