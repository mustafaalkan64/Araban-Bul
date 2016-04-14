using Araba.Core.Domain.DbTables;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Araba.Data.Context
{
    public class ArabaContext : DbContext
    {
        public ArabaContext()
            : base("ArabaContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<BodyType> BodyType { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<FuelType> FuelType { get; set; }
        public virtual DbSet<GearType> GearType { get; set; }
        public virtual DbSet<CarImage> CarImage { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<PlateType> PlateType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UsageStatus> UsageStatus { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Version> Version { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Car>()
                    .HasRequired(u => u.PublishUser)
                    .WithMany(g => g.Cars)
                    .HasForeignKey(u => u.PublishUserId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.FavoriteCars)
                        .WithMany(g => g.FavoriteMarkedUsers)
                        .Map(m => m.MapLeftKey("UserId")
                                   .MapRightKey("CarId")
                                   .ToTable("UserFavoriteCars"));
        }

        public class SeedBestContext : CreateDatabaseIfNotExists<ArabaContext>
        {
            protected override void Seed(ArabaContext context)
            {
                base.Seed(context);
            }
        }
    }
}
