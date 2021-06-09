using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {
        //what base class do: the constructor of base class
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }
       // public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasMany(m => m.Genre).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre",
                    m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                    g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));


            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>("UserRole",
                    u => u.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"));

            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Favorite>(ConfigureFavorites);
            
            //modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            // base.OnModelCreating(modelBuilder);
        }

        private void ConfigureFavorites(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => new { f.MovieId, f.UserId });
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PurchaseNumber).ValueGeneratedOnAdd();
            builder.HasIndex(p => new { p.UserId, p.MovieId }).IsUnique();
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).HasMaxLength(20);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            // specify your Fluent API rules.
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.CastId, mc.MovieId, mc.Character });
            builder.Property(mc => mc.Character).HasMaxLength(450);
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCast).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCast).HasForeignKey(mc => mc.CastId);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            // specify your Fluent API rules.
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Name);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.TmdbUrl).HasMaxLength(2084);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
         
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            // specify your Fluent API rules.
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(2084);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            builder.Ignore(m => m.Rating);
        }


        //private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        //{
        //    builder.ToTable("MovieGenre");
        //    builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
          
        //}
        private void ConfigureGenre(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");
            builder.HasKey(g => g.Id);
            builder.Property(t => t.Name).HasMaxLength(64);
        }

            private void ConfigureUser(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);

                builder.HasIndex(u => u.Email).IsUnique();
                builder.Property(u => u.Email).HasMaxLength(256);
                builder.Property(u => u.FirstName).HasMaxLength(128);
                builder.Property(u => u.LastName).HasMaxLength(128);
                builder.Property(u => u.HashedPassword).HasMaxLength(1024);
                builder.Property(u => u.PhoneNumber).HasMaxLength(16);
                builder.Property(u => u.Salt).HasMaxLength(1024);
                builder.Property(u => u.IsLocked).HasDefaultValue(false);

            }

            private void ConfigureReview(EntityTypeBuilder<Review> builder)
            {
                builder.ToTable("Review");
                builder.HasKey(r => new { r.MovieId, r.UserId });
                builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
                builder.Property(m => m.ReviewText).HasMaxLength(2084);
        }
        

    }
}
