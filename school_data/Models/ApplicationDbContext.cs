﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace School_Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public static string ConnectionString;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(ApplicationDbContext.ConnectionString);
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=quico\\sql2014;Database=SchoolDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        //    }
        //    //optionsBuilder.UseSqlServer(@"Server=quico\\sql2014;Database=SchoolDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        //    //base.OnConfiguring(optionsBuilder);

        //}
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    //base.OnModelCreating(builder);
        //    //var model = new RegistrationModel { UserName = "test" };

        //    //// Error There
        //    ////var result = await _userManager.CreateAsync(user, "1234");
        //    // _authService.Registeration(model, UserRoles.Admin);
        //    base.OnModelCreating(builder);
        //    // any unique string id
        //    const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        //    const string ROLE_ID = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

        //    builder.Entity<IdentityRole>().HasData(new IdentityRole
        //    {
        //        Id = ROLE_ID,
        //        Name = "Admin",
        //        NormalizedName = "Admin"
        //    });

        //    var hasher = new PasswordHasher<ApplicationUser>();
        //    builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        //    {
        //        Id = ADMIN_ID,
        //        UserName = "Admin",
        //        NormalizedUserName = "Admin",
        //        Email = "admin@gmail.com",
        //        NormalizedEmail = "admin@gmail.com",
        //        EmailConfirmed = false,
        //        PasswordHash = hasher.HashPassword(null, "Admin123#"),
        //        SecurityStamp = string.Empty
        //    });

        //    builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        //    {
        //        RoleId = ROLE_ID,
        //        UserId = ADMIN_ID
        //    });
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // any unique string id
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Admin",
                NormalizedName = "Admin"
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "Admin",
                NormalizedUserName = "Admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin123#"),
                SecurityStamp = string.Empty,
                CompleteName = "Administrator"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}