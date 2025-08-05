using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.Intranet.Organization.Data;

internal sealed class OrganizationDataContext(DbContextOptions<OrganizationDataContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users { get; set; }
    
    public DbSet<ApplicationUserRole> UserRoles { get; set; }
    
    public DbSet<ApplicationUserInRole> UserInRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDataContext).Assembly);
    }
}