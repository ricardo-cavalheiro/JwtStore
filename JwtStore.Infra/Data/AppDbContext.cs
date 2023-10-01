using Microsoft.EntityFrameworkCore;

using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Infra.Contexts.AccountContext.Mappings;

namespace JwtStore.Infra.Data;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; }

  public DbSet<Role> Roles { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserMap());
    modelBuilder.ApplyConfiguration(new RoleMap());
  }
}