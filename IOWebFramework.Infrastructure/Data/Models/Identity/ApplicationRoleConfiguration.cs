﻿using IOWebFramework.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IOWebFramework.Infrastructure.Data.Models.Identity
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("identity_roles")
                .HasKey(role => role.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("role_name_index").IsUnique();

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();

            builder.Property(p => p.Id)
                .HasColumnName("id");
            builder.Property(p => p.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp")
                .IsConcurrencyToken();
            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(256);
            builder.Property(p => p.NormalizedName)
                .HasColumnName("normalized_name")
                .HasMaxLength(256);

            builder.HasData(
                new ApplicationRole { NormalizedName = CommonConstant.Role.HR.ToUpper(), Name = CommonConstant.Role.HR, Id = "1" },
                new ApplicationRole { NormalizedName = CommonConstant.Role.Employee.ToUpper(), Name = CommonConstant.Role.Employee, Id = "2" },
                new ApplicationRole { NormalizedName = CommonConstant.Role.SuperAdmin.ToUpper(), Name = CommonConstant.Role.SuperAdmin, Id = "3" });
        }
    }
}
