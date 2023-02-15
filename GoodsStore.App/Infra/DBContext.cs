using GoodsStore.App.Models;
using GoodsStore.App.Models.AccessManagement;
using GoodsStore.App.Models.AccessManagement.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Infra
{
    public class DBContext : IdentityDbContext<User>
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Access Management
            modelBuilder.Entity<User>().ToTable("IAM_User", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("IAM_Role", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("IAM_UserRole", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("IAM_UserToken", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("IAM_UserLogin", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("IAM_UserClaim", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("IAM_RoleClaim", "dbo");

            modelBuilder.Entity<RoleFeature>().ToTable("IAM_RoleFeature", "dbo");
            modelBuilder.Entity<RoleFeature>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<RoleFeature>()
                .HasOne(t => t.Role);
            modelBuilder.Entity<RoleFeature>()
                .HasOne(t => t.Feature);

            modelBuilder.Entity<Feature>().ToTable("IAM_Feature", "dbo");
            modelBuilder.Entity<Feature>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Feature>()
                .HasOne(t => t.Module);

            modelBuilder.Entity<Module>().ToTable("IAM_Module", "dbo");
            modelBuilder.Entity<Module>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Module>()
                .HasMany(t => t.Features)
                .WithOne(t => t.Module);

            modelBuilder.Entity<Menu>().ToTable("IAM_Menu", "dbo");
            modelBuilder.Entity<Menu>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Menu>()
                .HasMany(t => t.Modules)
                .WithOne(t => t.Menu);

            modelBuilder.Entity<MainMenu>().ToTable("IAM_MainMenu", "dbo");
            modelBuilder.Entity<MainMenu>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<MainMenu>()
                .HasMany(t => t.Menus)
                .WithOne(t => t.MainMenu);
            #endregion Access Management

            #region Order Management
            modelBuilder.Entity<Product>().ToTable("OCM_Product", "dbo");
            modelBuilder.Entity<Product>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Order>().ToTable("OCM_Order", "dbo");
            modelBuilder.Entity<Order>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Order>()
                .HasMany(t => t.Items)
                .WithOne(t => t.Order);
            modelBuilder.Entity<Order>()
                .HasOne(t => t.Customer);

            modelBuilder.Entity<OrderItem>().ToTable("OCM_OrderItem", "dbo");
            modelBuilder.Entity<OrderItem>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<OrderItem>()
                .HasOne(t => t.Order);
            modelBuilder.Entity<OrderItem>()
                .HasOne(t => t.Product);

            modelBuilder.Entity<Customer>().ToTable("OCM_Customer", "dbo");
            modelBuilder.Entity<Customer>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Customer>()
                .HasOne(t => t.User);
            #endregion Order Management


            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
