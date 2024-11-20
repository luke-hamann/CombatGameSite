using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Models
{
    public class CombatContext : DbContext
    {
        public CombatContext(DbContextOptions<CombatContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "alice",
                    Password = "password",
                    Tagline = "Hello, world!"
                },
                new User
                {
                    Id = 2,
                    Name = "bob",
                    Password = "password",
                    Tagline = "Catchphrase."
                },
                new User
                {
                    Id = 3,
                    Name = "charlie",
                    Password = "password",
                    Tagline = "Super smashing success."
                },
                new User
                {
                    Id = 4,
                    Name = "dave",
                    Password = "password",
                    Tagline = "Game on."
                }
            );

            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = "H1", TypeId = 1, Cost = 10, Name = "Healing Wave", Description = "A soothing wave of water heals the target.", Attack = 0, DefenseMultiplier = 1.2 },
                new Skill { Id = "A1", TypeId = 2, Cost = 15, Name = "Fireball", Description = "A blazing sphere of fire strikes the enemy.", Attack = 20, DefenseMultiplier = 1.0 },
                new Skill { Id = "D1", TypeId = 3, Cost = 12, Name = "Wind Barrier", Description = "A swirling wind protects against attacks.", Attack = 0, DefenseMultiplier = 1.5 },
                new Skill { Id = "A2", TypeId = 2, Cost = 18, Name = "Inferno Burst", Description = "A fiery explosion dealing massive damage.", Attack = 25, DefenseMultiplier = 1.0 },
                new Skill { Id = "H2", TypeId = 1, Cost = 8, Name = "Aqua Surge", Description = "A small burst of water to heal minor wounds.", Attack = 0, DefenseMultiplier = 1.1 },
                new Skill { Id = "D2", TypeId = 4, Cost = 14, Name = "Stone Skin", Description = "Earth magic hardens the skin for defense.", Attack = 0, DefenseMultiplier = 1.8 },
                new Skill { Id = "A3", TypeId = 3, Cost = 20, Name = "Cyclone Slash", Description = "A cutting wind attack that pierces defenses.", Attack = 22, DefenseMultiplier = 1.0 },
                new Skill { Id = "D3", TypeId = 1, Cost = 9, Name = "Water Veil", Description = "Encases the target in a veil of water for protection.", Attack = 0, DefenseMultiplier = 1.4 },
                new Skill { Id = "H3", TypeId = 3, Cost = 11, Name = "Breeze of Life", Description = "A gentle wind revitalizes the target.", Attack = 0, DefenseMultiplier = 1.3 },
                new Skill { Id = "A4", TypeId = 4, Cost = 17, Name = "Earthquake Smash", Description = "An earth-shaking attack to crush enemies.", Attack = 24, DefenseMultiplier = 1.0 },
                new Skill { Id = "D4", TypeId = 2, Cost = 16, Name = "Flame Ward", Description = "A shield of fire reduces incoming damage.", Attack = 0, DefenseMultiplier = 1.6 },
                new Skill { Id = "H4", TypeId = 4, Cost = 13, Name = "Earthen Rejuvenation", Description = "The power of earth heals the target slowly.", Attack = 0, DefenseMultiplier = 1.2 },
                new Skill { Id = "A5", TypeId = 1, Cost = 19, Name = "Tidal Crush", Description = "A wave crashes on the enemy for damage.", Attack = 26, DefenseMultiplier = 1.0 },
                new Skill { Id = "D5", TypeId = 3, Cost = 10, Name = "Wind Fortress", Description = "A massive wind barrier blocks attacks.", Attack = 0, DefenseMultiplier = 1.7 },
                new Skill { Id = "H5", TypeId = 2, Cost = 12, Name = "Flare Heal", Description = "A fiery warmth restores the target's health.", Attack = 0, DefenseMultiplier = 1.1 },
                new Skill { Id = "A6", TypeId = 4, Cost = 21, Name = "Landslide Strike", Description = "Rocks pummel the enemy, causing severe damage.", Attack = 28, DefenseMultiplier = 1.0 },
                new Skill { Id = "D6", TypeId = 1, Cost = 15, Name = "Aqua Shield", Description = "A shield of water dampens damage.", Attack = 0, DefenseMultiplier = 1.5 },
                new Skill { Id = "H6", TypeId = 3, Cost = 10, Name = "Soothing Breeze", Description = "A calming wind that heals minor injuries.", Attack = 0, DefenseMultiplier = 1.1 },
                new Skill { Id = "A7", TypeId = 2, Cost = 22, Name = "Flaming Blade", Description = "A sword engulfed in fire slashes the enemy.", Attack = 30, DefenseMultiplier = 1.0 },
                new Skill { Id = "D7", TypeId = 4, Cost = 13, Name = "Rock Wall", Description = "A massive wall of earth defends against attacks.", Attack = 0, DefenseMultiplier = 1.9 },
                new Skill { Id = "H7", TypeId = 4, Cost = 14, Name = "Healing Pebbles", Description = "Tiny stones infused with healing power.", Attack = 0, DefenseMultiplier = 1.2 },
                new Skill { Id = "A8", TypeId = 1, Cost = 25, Name = "Aqua Slash", Description = "A sharp blade of water cuts through the enemy.", Attack = 32, DefenseMultiplier = 1.0 },
                new Skill { Id = "D8", TypeId = 2, Cost = 18, Name = "Blazing Barrier", Description = "A burning shield reduces damage taken.", Attack = 0, DefenseMultiplier = 1.7 },
                new Skill { Id = "H8", TypeId = 3, Cost = 12, Name = "Whispering Wind", Description = "A soft breeze heals and rejuvenates.", Attack = 0, DefenseMultiplier = 1.2 },
                new Skill { Id = "A9", TypeId = 3, Cost = 23, Name = "Tempest Fang", Description = "A ferocious wind slashes the enemy.", Attack = 33, DefenseMultiplier = 1.0 },
                new Skill { Id = "D9", TypeId = 1, Cost = 15, Name = "Ocean Shield", Description = "A watery shield protects the user.", Attack = 0, DefenseMultiplier = 1.6 },
                new Skill { Id = "H9", TypeId = 2, Cost = 10, Name = "Flame Pulse", Description = "A quick burst of fire heals the target.", Attack = 0, DefenseMultiplier = 1.1 },
                new Skill { Id = "A10", TypeId = 4, Cost = 24, Name = "Boulder Bash", Description = "A giant boulder crushes the opponent.", Attack = 34, DefenseMultiplier = 1.0 },
                new Skill { Id = "D10", TypeId = 3, Cost = 19, Name = "Cyclone Guard", Description = "A rotating wind reduces damage.", Attack = 0, DefenseMultiplier = 1.8 },
                new Skill { Id = "H10", TypeId = 1, Cost = 14, Name = "Rain of Healing", Description = "A gentle rain restores the target's health.", Attack = 0, DefenseMultiplier = 1.3 },
                new Skill { Id = "A11", TypeId = 2, Cost = 26, Name = "Flame Surge", Description = "A fiery wave burns all in its path.", Attack = 35, DefenseMultiplier = 1.0 },
                new Skill { Id = "D11", TypeId = 4, Cost = 20, Name = "Rock Shield", Description = "Earth barriers defend against attacks.", Attack = 0, DefenseMultiplier = 1.8 },
                new Skill { Id = "H11", TypeId = 3, Cost = 13, Name = "Winds of Renewal", Description = "A rejuvenating wind heals all nearby allies.", Attack = 0, DefenseMultiplier = 1.3 },
                new Skill { Id = "A12", TypeId = 1, Cost = 27, Name = "Ocean Blast", Description = "A torrent of water smashes the enemy.", Attack = 36, DefenseMultiplier = 1.0 },
                new Skill { Id = "D12", TypeId = 2, Cost = 18, Name = "Lava Shield", Description = "A barrier of molten fire absorbs damage.", Attack = 0, DefenseMultiplier = 1.7 },
                new Skill { Id = "H12", TypeId = 4, Cost = 16, Name = "Earth Embrace", Description = "The earth's power restores health gradually.", Attack = 0, DefenseMultiplier = 1.2 },
                new Skill { Id = "A13", TypeId = 3, Cost = 29, Name = "Storm Strike", Description = "A devastating lightning-charged wind attack.", Attack = 37, DefenseMultiplier = 1.0 },
                new Skill { Id = "D13", TypeId = 1, Cost = 20, Name = "Bubble Shield", Description = "Encases the user in a protective bubble.", Attack = 0, DefenseMultiplier = 1.6 },
                new Skill { Id = "H13", TypeId = 2, Cost = 12, Name = "Ember Heal", Description = "A healing ember that soothes wounds.", Attack = 0, DefenseMultiplier = 1.1 }
            );
        }
    }
}
