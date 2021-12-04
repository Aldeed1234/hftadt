using ALDEED_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Data
{
    public class VideoGameDbContext : DbContext
    {
        //table creation
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Planets> Planets { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<PvpEvent> PvpEvent { get; set; }

        public VideoGameDbContext()
        {
            this.Database?.EnsureCreated();
        }

        public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        //edited connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            }
        }

        //model creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Locations canyon = new Locations() { LocationId = 1, LocationName = "Canyon"};
            Locations mountain = new Locations() { LocationId = 2, LocationName = "Mountain"};
            Locations city = new Locations() { LocationId = 3, LocationName = "City" };
            Locations tower = new Locations() { LocationId = 4, LocationName = "Tower" };

            PvpEvent thecrucible = new PvpEvent() { PvpEventId = 1, PvpEventName = "The Crucible", LocationId = 1 };
            PvpEvent arena = new PvpEvent() { PvpEventId = 2, PvpEventName = "Arena", LocationId = 2 };
            PvpEvent kingofthehill = new PvpEvent() { PvpEventId = 3, PvpEventName = "King of the Hill", LocationId = 2 };
            PvpEvent payload = new PvpEvent() { PvpEventId = 4, PvpEventName = "Payload", LocationId = 4 };
            PvpEvent deathmatch = new PvpEvent() { PvpEventId = 5, PvpEventName = "Deathmatch", LocationId = 3 };
            PvpEvent battleroyale = new PvpEvent() { PvpEventId = 6, PvpEventName = "Battle Royale", LocationId = 1 };

            Players p1 = new Players() { PlayerId = 1, PlayerName = "Parzival", PlayerLevel = 10, IsEliminated = false, PvpEventId = 1 };
            Players p2 = new Players() { PlayerId = 2, PlayerName = "H", PlayerLevel = 13, IsEliminated = true, EliminatedOnPlanet_PlanetId= 2, PvpEventId = 2 };
            Players p3 = new Players() { PlayerId = 3, PlayerName = "Artemis", PlayerLevel = 20, IsEliminated = false, PvpEventId = 1 };
            Players p4 = new Players() { PlayerId = 4, PlayerName = "ASDSAS", PlayerLevel = 9, IsEliminated = false, PvpEventId = 2 };
            Players p5 = new Players() { PlayerId = 5, PlayerName = "iceman", PlayerLevel = 11, IsEliminated = true, EliminatedOnPlanet_PlanetId=3, PvpEventId = 6 };
            Players p6 = new Players() { PlayerId = 4, PlayerName = "weedman", PlayerLevel = 12, IsEliminated = true, EliminatedOnPlanet_PlanetId=2, PvpEventId = 3 };
            Players p7 = new Players() { PlayerId = 4, PlayerName = "weedwoman", PlayerLevel = 13, IsEliminated = false, PvpEventId = 4 };
            Players p8 = new Players() { PlayerId = 4, PlayerName = "ASDSAS", PlayerLevel = 9, IsEliminated = true, EliminatedOnPlanet_PlanetId=1, PvpEventId = 5 };
            Players p9 = new Players() { PlayerId = 4, PlayerName = "ASDSAS", PlayerLevel = 9, IsEliminated = false, PvpEventId = 5 };

            Planets earth = new Planets() { PlanetId = 1, PlanetName = "Earth", Difficulty = 1 };
            Planets mars = new Planets() { PlanetId = 2, PlanetName = "Mars", Difficulty = 3 };
            Planets venus = new Planets() { PlanetId = 3, PlanetName = "Venus", Difficulty = 2 };

            SaveChanges();

            //fluent API
            modelBuilder.Entity<PvpEvent>(entity =>
            {
                entity.HasOne(pvpevent => pvpevent.Location)
                .WithMany(location => location.PvpEvents)
                .HasForeignKey(pvpevent => pvpevent.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasOne(player => player.PvpEvent)
                .WithMany(pvpEvent => pvpEvent.Players)
                .HasForeignKey(player => player.PvpEventId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasOne(player => player.Planets)
                .WithMany(planet => planet.Players)
                .HasForeignKey(player => player.EliminatedOnPlanet_PlanetId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //Adding the data
            modelBuilder.Entity<Locations>().HasData(canyon, mountain, city, tower);
            modelBuilder.Entity<PvpEvent>().HasData(thecrucible, arena, kingofthehill, payload, deathmatch, battleroyale);
            modelBuilder.Entity<Players>().HasData(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            modelBuilder.Entity<Planets>().HasData(earth, mars, venus);

        }
    }
}
