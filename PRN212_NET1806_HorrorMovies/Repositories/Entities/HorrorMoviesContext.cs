using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repositories.Entities;

public partial class HorrorMoviesContext : DbContext
{
    public HorrorMoviesContext()
    {
    }

    public HorrorMoviesContext(DbContextOptions<HorrorMoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CriticReview> CriticReviews { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<UserReview> UserReviews { get; set; }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CriticReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__CriticRe__2ECD6E04F07C4348");

            entity.ToTable("CriticReview");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("reviewId");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.CriticName).HasColumnName("criticName");
            entity.Property(e => e.IsTopCritic).HasColumnName("isTopCritic");
            entity.Property(e => e.MovieId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("movieId");
            entity.Property(e => e.PublicationName).HasColumnName("publicationName");
            entity.Property(e => e.Quote).HasColumnName("quote");
            entity.Property(e => e.ReviewUrl).HasColumnName("reviewUrl");
            entity.Property(e => e.ScoreSentiment).HasColumnName("scoreSentiment");

            entity.HasOne(d => d.Movie).WithMany(p => p.CriticReviews)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__CriticRev__movie__3B75D760");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__42EB374EE4BE64C6");

            entity.Property(e => e.MovieId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("movieId");
            entity.Property(e => e.MovieRank).HasColumnName("movieRank");
            entity.Property(e => e.MovieTitle).HasColumnName("movieTitle");
            entity.Property(e => e.MovieUrl).HasColumnName("movieURL");
            entity.Property(e => e.MovieYear).HasColumnName("movieYear");
        });

        modelBuilder.Entity<UserReview>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserRevi__CB9A1CFF398AAF8A");

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("userId");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.HasProfanity).HasColumnName("hasProfanity");
            entity.Property(e => e.HasSpoilers).HasColumnName("hasSpoilers");
            entity.Property(e => e.IsSuperReviewer).HasColumnName("isSuperReviewer");
            entity.Property(e => e.MovieId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("movieId");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Movie).WithMany(p => p.UserReviews)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__UserRevie__movie__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
