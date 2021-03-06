﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using AuthWebAPI.Helpers;

namespace AuthWebAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ModelContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
            //Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var identityUserTableName = AppSettingsHelper.GetValue<string>("Identity.IdentityUserTable.Name", () => string.Empty);
            var identityRoleTableName = AppSettingsHelper.GetValue<string>("Identity.IdentityRoleTable.Name", () => string.Empty);
            var identityUserRoleTableName = AppSettingsHelper.GetValue<string>("Identity.IdentityUserRoleTable.Name", () => string.Empty);
            var identityUserClaimTableName = AppSettingsHelper.GetValue<string>("Identity.IdentityUserClaimTable.Name", () => string.Empty);
            var identityUserLoginTableName = AppSettingsHelper.GetValue<string>("Identity.IdentityUserLoginTable.Name", () => string.Empty);

            if (!string.IsNullOrEmpty(identityUserTableName))
                modelBuilder.Entity<ApplicationUser>().ToTable(identityUserTableName);

            if (!string.IsNullOrEmpty(identityRoleTableName))
                modelBuilder.Entity<IdentityRole>().ToTable(identityRoleTableName);

            if (!string.IsNullOrEmpty(identityUserRoleTableName))
                modelBuilder.Entity<IdentityUserRole>().ToTable(identityUserRoleTableName);

            if (!string.IsNullOrEmpty(identityUserClaimTableName))
                modelBuilder.Entity<IdentityUserClaim>().ToTable(identityUserClaimTableName);

            if (!string.IsNullOrEmpty(identityUserLoginTableName))
                modelBuilder.Entity<IdentityUserLogin>().ToTable(identityUserLoginTableName);
        }
    }
}