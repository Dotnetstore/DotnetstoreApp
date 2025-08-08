using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.Intranet.Organization.Data;

public sealed class OrganizationDataContext : DbContext
{
    public OrganizationDataContext(DbContextOptions<OrganizationDataContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
    
    public DbSet<ApplicationUserInRole> ApplicationUserInRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDataContext).Assembly);
    }
}