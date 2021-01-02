using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Seat> Seats  { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        public DbSet<ActorMovie> ActorMovie { get; set; }

        public DbSet<CinemaMovie> CinemaMovie { get; set; }

        public DbSet<SeatDate> SeatDate { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorMovie>()
                .HasKey(e => new { e.actorId, e.movieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(e => e.actor)
                .WithMany(e => e.movies)
                .HasForeignKey(e => e.actorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(e => e.movie)
                .WithMany(e => e.actors)
                .HasForeignKey(e => e.movieId);

            modelBuilder.Entity<CinemaMovie>()
                .HasKey(e => new { e.cinemaId, e.movieId });

            modelBuilder.Entity<CinemaMovie>()
                .HasOne(e => e.cinema)
                .WithMany(e => e.movies)
                .HasForeignKey(e => e.cinemaId);


            modelBuilder.Entity<CinemaMovie>()
                .HasOne(e => e.movie)
                .WithMany(e => e.cinemas)
                .HasForeignKey(e => e.movieId);
        }
    }
}
