using JwtStore.Core.Contexts.AccountContext.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("User");

    builder.HasKey(x => x.Id);

    builder
      .Property(x => x.Name)
      .HasColumnName("Name")
      .HasColumnType("NVARCHAR")
      .HasMaxLength(120)
      .IsRequired();

    builder
      .Property(x => x.Image)
      .HasColumnName("Image")
      .HasColumnType("VARCHAR")
      .HasMaxLength(255)
      .IsRequired();

    builder
      .OwnsOne(x => x.Email)
      .Property(x => x.Address)
      .HasColumnName("Email")
      .IsRequired();

    builder
      .OwnsOne(x => x.Email)
      .OwnsOne(x => x.Verifier)
      .Property(x => x.Code)
      .HasColumnName("Email_Verfication_Code")
      .IsRequired();

    builder
      .OwnsOne(x => x.Email)
      .OwnsOne(x => x.Verifier)
      .Property(x => x.ExpiresAt)
      .HasColumnName("Email_Verification_Expires_At")
      .IsRequired(false);

    builder
      .OwnsOne(x => x.Email)
      .OwnsOne(x => x.Verifier)
      .Property(x => x.VerifiedAt)
      .HasColumnName("Email_Verification_Verified_At")
      .IsRequired(false);

    builder
      .OwnsOne(x => x.Email)
      .OwnsOne(x => x.Verifier)
      .Ignore(x => x.IsActive);

    builder
      .OwnsOne(x => x.Password)
      .Property(x => x.Hash)
      .HasColumnName("Password_Hash")
      .IsRequired();

    builder
      .OwnsOne(x => x.Password)
      .Property(x => x.ResetCode)
      .HasColumnName("Password_Reset_Code")
      .IsRequired();

    builder
      .HasMany(x => x.Roles)
      .WithMany(x => x.Users)
      .UsingEntity<Dictionary<string, object>>(
        "UserRole",
        role => role
          .HasOne<Role>()
          .WithMany()
          .HasForeignKey("RoleId")
          .OnDelete(DeleteBehavior.Cascade),
        user => user
          .HasOne<User>()
          .WithMany()
          .HasForeignKey("UserId")
          .OnDelete(DeleteBehavior.Cascade)
      );
  }
}