using System.Configuration;
using EFInfrastructure.Persistence.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EFInfrastructure.Contexts
{
    public class MyDbContext : DbContext
    {
        public static DbContextOptionsBuilder OptionsBuilder
        {
            get
            {
                var contextOptionBuilder = new DbContextOptionsBuilder<MyDbContext>();
                var connectionString = "Server=(localdb)\\mssqllocaldb;Database=ItdddContext-1;Trusted_Connection=True;MultipleActiveResultSets=true";

                contextOptionBuilder.UseSqlServer(connectionString);

                return contextOptionBuilder;
            }
        }
        public static MyDbContext Create()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseSqlServer(ConfigurationManager.ConnectionStrings["FooConnection"].ConnectionString);
            var options = builder.Options;
            var context = new MyDbContext(options);

            return context;
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public MyDbContext() : base(OptionsBuilder.Options)
        {

        }
        public DbSet<CircleDataModel> Circles { get; set; }
        public DbSet<UserCircle> UserCircles { get; set; }

        public DbSet<UserDataModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDataModel>()
                .HasMany(x => x.OwnedCircles)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserCircle>()
                .HasKey(x => new { x.UserId, x.CircleId });
            modelBuilder.Entity<UserCircle>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.MemberOf)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserCircle>()
                .HasOne(uc => uc.Circle)
                .WithMany(c => c.CircleMembers)
                .HasForeignKey(uc => uc.CircleId);

        }
    }


}
