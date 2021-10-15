using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace EndpointAPI.Model
{
    public partial class MultiTracksDBContext : DbContext
    {
        public MultiTracksDBContext()
        {
        }

        public MultiTracksDBContext(DbContextOptions<MultiTracksDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<AssessmentStep> AssessmentSteps { get; set; }
        public virtual DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
                var connectionString = configuration.GetConnectionString("MultitracksConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.Property(e => e.AlbumId).HasColumnName("albumID");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dateCreation")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.Biography)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("biography");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dateCreation")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HeroUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("heroURL");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<AssessmentStep>(entity =>
            {
                entity.HasKey(e => e.StepId)
                    .HasName("PK__Assessme__4E25C23D66152F0B");

                entity.Property(e => e.StepId).HasColumnName("stepID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("text");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.Property(e => e.SongId).HasColumnName("songID");

                entity.Property(e => e.AlbumId).HasColumnName("albumID");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.Bpm)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("bpm");

                entity.Property(e => e.Chart).HasColumnName("chart");

                entity.Property(e => e.CustomMix).HasColumnName("customMix");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dateCreation")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Multitracks).HasColumnName("multitracks");

                entity.Property(e => e.Patches).HasColumnName("patches");

                entity.Property(e => e.ProPresenter).HasColumnName("proPresenter");

                entity.Property(e => e.RehearsalMix).HasColumnName("rehearsalMix");

                entity.Property(e => e.SongSpecificPatches).HasColumnName("songSpecificPatches");

                entity.Property(e => e.TimeSignature)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("timeSignature");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
