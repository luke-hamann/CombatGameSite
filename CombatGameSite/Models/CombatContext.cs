using Microsoft.EntityFrameworkCore;

namespace CombatGameSite.Models
{
    public class CombatContext(DbContextOptions<CombatContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<Skill> Skills { get; set; }
        public required DbSet<Character> Characters { get; set; }
        public required DbSet<Team> Teams { get; set; }

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
                new Skill { Id = "H1", TypeId = 1, Cost = 10, Name = "Healing Wave", Description = "A soothing wave of water heals the target.", Attack = 0 },
                new Skill { Id = "A1", TypeId = 2, Cost = 15, Name = "Fireball", Description = "A blazing sphere of fire strikes the enemy.", Attack = 20 },
                new Skill { Id = "D1", TypeId = 3, Cost = 12, Name = "Wind Barrier", Description = "A swirling wind protects against attacks.", Attack = 0 },
                new Skill { Id = "A2", TypeId = 2, Cost = 18, Name = "Inferno Burst", Description = "A fiery explosion dealing massive damage.", Attack = 25 },
                new Skill { Id = "H2", TypeId = 1, Cost = 8, Name = "Aqua Surge", Description = "A small burst of water to heal minor wounds.", Attack = 0 },
                new Skill { Id = "D2", TypeId = 4, Cost = 14, Name = "Stone Skin", Description = "Earth magic hardens the skin for defense.", Attack = 0 },
                new Skill { Id = "A3", TypeId = 3, Cost = 20, Name = "Cyclone Slash", Description = "A cutting wind attack that pierces defenses.", Attack = 22 },
                new Skill { Id = "D3", TypeId = 1, Cost = 9, Name = "Water Veil", Description = "Encases the target in a veil of water for protection.", Attack = 0 },
                new Skill { Id = "H3", TypeId = 3, Cost = 11, Name = "Breeze of Life", Description = "A gentle wind revitalizes the target.", Attack = 0 },
                new Skill { Id = "A4", TypeId = 4, Cost = 17, Name = "Earthquake Smash", Description = "An earth-shaking attack to crush enemies.", Attack = 24 },
                new Skill { Id = "D4", TypeId = 2, Cost = 16, Name = "Flame Ward", Description = "A shield of fire reduces incoming damage.", Attack = 0 },
                new Skill { Id = "H4", TypeId = 4, Cost = 13, Name = "Earthen Rejuvenation", Description = "The power of earth heals the target slowly.", Attack = 0 },
                new Skill { Id = "A5", TypeId = 1, Cost = 19, Name = "Tidal Crush", Description = "A wave crashes on the enemy for damage.", Attack = 26 },
                new Skill { Id = "D5", TypeId = 3, Cost = 10, Name = "Wind Fortress", Description = "A massive wind barrier blocks attacks.", Attack = 0 },
                new Skill { Id = "H5", TypeId = 2, Cost = 12, Name = "Flare Heal", Description = "A fiery warmth restores the target's health.", Attack = 0 },
                new Skill { Id = "A6", TypeId = 4, Cost = 21, Name = "Landslide Strike", Description = "Rocks pummel the enemy, causing severe damage.", Attack = 28 },
                new Skill { Id = "D6", TypeId = 1, Cost = 15, Name = "Aqua Shield", Description = "A shield of water dampens damage.", Attack = 0 },
                new Skill { Id = "H6", TypeId = 3, Cost = 10, Name = "Soothing Breeze", Description = "A calming wind that heals minor injuries.", Attack = 0 },
                new Skill { Id = "A7", TypeId = 2, Cost = 22, Name = "Flaming Blade", Description = "A sword engulfed in fire slashes the enemy.", Attack = 30 },
                new Skill { Id = "D7", TypeId = 4, Cost = 13, Name = "Rock Wall", Description = "A massive wall of earth defends against attacks.", Attack = 0 },
                new Skill { Id = "H7", TypeId = 4, Cost = 14, Name = "Healing Pebbles", Description = "Tiny stones infused with healing power.", Attack = 0 },
                new Skill { Id = "A8", TypeId = 1, Cost = 25, Name = "Aqua Slash", Description = "A sharp blade of water cuts through the enemy.", Attack = 32 },
                new Skill { Id = "D8", TypeId = 2, Cost = 18, Name = "Blazing Barrier", Description = "A burning shield reduces damage taken.", Attack = 0 },
                new Skill { Id = "H8", TypeId = 3, Cost = 12, Name = "Whispering Wind", Description = "A soft breeze heals and rejuvenates.", Attack = 0 },
                new Skill { Id = "A9", TypeId = 3, Cost = 23, Name = "Tempest Fang", Description = "A ferocious wind slashes the enemy.", Attack = 33 },
                new Skill { Id = "D9", TypeId = 1, Cost = 15, Name = "Ocean Shield", Description = "A watery shield protects the user.", Attack = 0 },
                new Skill { Id = "H9", TypeId = 2, Cost = 10, Name = "Flame Pulse", Description = "A quick burst of fire heals the target.", Attack = 0 },
                new Skill { Id = "A10", TypeId = 4, Cost = 24, Name = "Boulder Bash", Description = "A giant boulder crushes the opponent.", Attack = 34 },
                new Skill { Id = "D10", TypeId = 3, Cost = 19, Name = "Cyclone Guard", Description = "A rotating wind reduces damage.", Attack = 0 },
                new Skill { Id = "H10", TypeId = 1, Cost = 14, Name = "Rain of Healing", Description = "A gentle rain restores the target's health.", Attack = 0 },
                new Skill { Id = "A11", TypeId = 2, Cost = 26, Name = "Flame Surge", Description = "A fiery wave burns all in its path.", Attack = 35 },
                new Skill { Id = "D11", TypeId = 4, Cost = 20, Name = "Rock Shield", Description = "Earth barriers defend against attacks.", Attack = 0 },
                new Skill { Id = "H11", TypeId = 3, Cost = 13, Name = "Winds of Renewal", Description = "A rejuvenating wind heals all nearby allies.", Attack = 0 },
                new Skill { Id = "A12", TypeId = 1, Cost = 27, Name = "Ocean Blast", Description = "A torrent of water smashes the enemy.", Attack = 36 },
                new Skill { Id = "D12", TypeId = 2, Cost = 18, Name = "Lava Shield", Description = "A barrier of molten fire absorbs damage.", Attack = 0 },
                new Skill { Id = "H12", TypeId = 4, Cost = 16, Name = "Earth Embrace", Description = "The earth's power restores health gradually.", Attack = 0 },
                new Skill { Id = "A13", TypeId = 3, Cost = 29, Name = "Storm Strike", Description = "A devastating lightning-charged wind attack.", Attack = 37 },
                new Skill { Id = "D13", TypeId = 1, Cost = 20, Name = "Bubble Shield", Description = "Encases the user in a protective bubble.", Attack = 0 },
                new Skill { Id = "H13", TypeId = 2, Cost = 12, Name = "Ember Heal", Description = "A healing ember that soothes wounds.", Attack = 0 }
            );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, UserId = 1, Name = "Aqua Warrior", TypeId = 2, Health = 100, Species = "Human", SkillPrimaryId = "A1", SkillSecondaryId = "A2", SkillTertiaryId = "H1" },
                new Character { Id = 2, UserId = 2, Name = "Flame Destroyer", TypeId = 4, Health = 100, Species = "Elf", SkillPrimaryId = "A3", SkillSecondaryId = "A4", SkillTertiaryId = "H1" },
                new Character { Id = 3, UserId = 3, Name = "Wind Ninja", TypeId = 4, Health = 100, Species = "Dwarf", SkillPrimaryId = "A5", SkillSecondaryId = "A6", SkillTertiaryId = "H1" },
                new Character { Id = 4, UserId = 4, Name = "Earth Titan", TypeId = 2, Health = 100, Species = "Human", SkillPrimaryId = "A7", SkillSecondaryId = "A8", SkillTertiaryId = "H1" },
                new Character { Id = 5, UserId = 1, Name = "Water Sage", TypeId = 1, Health = 100, Species = "Elf", SkillPrimaryId = "A9", SkillSecondaryId = "A10", SkillTertiaryId = "H1" },
                new Character { Id = 6, UserId = 2, Name = "Fire Knight", TypeId = 3, Health = 100, Species = "Dwarf", SkillPrimaryId = "A11", SkillSecondaryId = "A12", SkillTertiaryId = "H1" },
                new Character { Id = 7, UserId = 3, Name = "Wind Shaman", TypeId = 2, Health = 100, Species = "Human", SkillPrimaryId = "A13", SkillSecondaryId = "A13", SkillTertiaryId = "H1" },
                new Character { Id = 8, UserId = 4, Name = "Earth Wizard", TypeId = 1, Health = 100, Species = "Elf", SkillPrimaryId = "H1", SkillSecondaryId = "H1", SkillTertiaryId = "H1" },
                new Character { Id = 9, UserId = 1, Name = "Water Spirit", TypeId = 4, Health = 100, Species = "Dwarf", SkillPrimaryId = "H1", SkillSecondaryId = "H1", SkillTertiaryId = "H1" },
                new Character { Id = 10, UserId = 2, Name = "Flame Phoenix", TypeId = 2, Health = 100, Species = "Human", SkillPrimaryId = "H1", SkillSecondaryId = "H1", SkillTertiaryId = "H1" },
                new Character { Id = 11, UserId = 3, Name = "Wind Warrior", TypeId = 3, Health = 100, Species = "Elf", SkillPrimaryId = "A1", SkillSecondaryId = "A2", SkillTertiaryId = "H2" },
                new Character { Id = 12, UserId = 4, Name = "Fire Sorcerer", TypeId = 4, Health = 100, Species = "Dwarf", SkillPrimaryId = "A3", SkillSecondaryId = "A4", SkillTertiaryId = "H2" },
                new Character { Id = 13, UserId = 1, Name = "Wind Elf", TypeId = 2, Health = 100, Species = "Human", SkillPrimaryId = "A5", SkillSecondaryId = "A6", SkillTertiaryId = "H2" },
                new Character { Id = 14, UserId = 2, Name = "Earth Golem", TypeId = 4, Health = 100, Species = "Elf", SkillPrimaryId = "A7", SkillSecondaryId = "A8", SkillTertiaryId = "H2" },
                new Character { Id = 15, UserId = 3, Name = "Water Knight", TypeId = 2, Health = 100, Species = "Dwarf", SkillPrimaryId = "A9", SkillSecondaryId = "A10", SkillTertiaryId = "H2" },
                new Character { Id = 16, UserId = 4, Name = "Fire Wizard", TypeId = 2, Health = 100, Species = "Human", SkillPrimaryId = "A11", SkillSecondaryId = "A12", SkillTertiaryId = "H2" },
                new Character { Id = 17, UserId = 1, Name = "Wind Shaman", TypeId = 3, Health = 100, Species = "Elf", SkillPrimaryId = "A13", SkillSecondaryId = "A13", SkillTertiaryId = "H2" },
                new Character { Id = 18, UserId = 2, Name = "Water Guardian", TypeId = 2, Health = 100, Species = "Dwarf", SkillPrimaryId = "H1", SkillSecondaryId = "H1", SkillTertiaryId = "H2" },
                new Character { Id = 19, UserId = 3, Name = "Flame Demon", TypeId = 1, Health = 100, Species = "Human", SkillPrimaryId = "H1", SkillSecondaryId = "H1", SkillTertiaryId = "H2" },
                new Character { Id = 20, UserId = 4, Name = "Wind Demon", TypeId = 2, Health = 100, Species = "Elf", SkillPrimaryId = "H1", SkillSecondaryId = "H1", SkillTertiaryId = "H2" },
                new Character { Id = 21, UserId = 1, Name = "Water Monk", TypeId = 2, Health = 100, Species = "Dwarf", SkillPrimaryId = "A1", SkillSecondaryId = "A2", SkillTertiaryId = "H3" },
                new Character { Id = 22, UserId = 2, Name = "Fire Samurai", TypeId = 1, Health = 100, Species = "Human", SkillPrimaryId = "A3", SkillSecondaryId = "A4", SkillTertiaryId = "H3" },
                new Character { Id = 23, UserId = 3, Name = "Wind Warrior", TypeId = 3, Health = 100, Species = "Elf", SkillPrimaryId = "A5", SkillSecondaryId = "A6", SkillTertiaryId = "H3" },
                new Character { Id = 24, UserId = 4, Name = "Earth Beast", TypeId = 1, Health = 100, Species = "Dwarf", SkillPrimaryId = "A7", SkillSecondaryId = "A8", SkillTertiaryId = "H3" },
                new Character { Id = 25, UserId = 1, Name = "Water Sorceress", TypeId = 3, Health = 100, Species = "Human", SkillPrimaryId = "A9", SkillSecondaryId = "A10", SkillTertiaryId = "H3" }
            );

            modelBuilder.Entity<Team>().HasData(
                // Teams for User 1
                new Team { Id = 1, UserId = 1, Character1Id = 1, Character2Id = 5, Character3Id = 9, Character4Id = 13, Character5Id = 17, Name = "Aqua Defenders", Score = 150 },
                new Team { Id = 2, UserId = 1, Character1Id = 1, Character2Id = 5, Character3Id = 9, Character4Id = 13, Character5Id = 21, Name = "Tidal Guardians", Score = 140 },
                new Team { Id = 3, UserId = 1, Character1Id = 9, Character2Id = 13, Character3Id = 17, Character4Id = 21, Character5Id = 25, Name = "Water Warriors", Score = 130 },

                // Teams for User 2
                new Team { Id = 4, UserId = 2, Character1Id = 2, Character2Id = 6, Character3Id = 10, Character4Id = 14, Character5Id = 18, Name = "Blazing Firestorm", Score = 160 },
                new Team { Id = 5, UserId = 2, Character1Id = 2, Character2Id = 6, Character3Id = 10, Character4Id = 14, Character5Id = 22, Name = "Inferno Knights", Score = 140 },
                new Team { Id = 6, UserId = 2, Character1Id = 6, Character2Id = 10, Character3Id = 14, Character4Id = 18, Character5Id = 22, Name = "Fire Warriors", Score = 135 },

                // Teams for User 3
                new Team { Id = 7, UserId = 3, Character1Id = 3, Character2Id = 7, Character3Id = 11, Character4Id = 15, Character5Id = 19, Name = "Wind Strikers", Score = 150 },
                new Team { Id = 8, UserId = 3, Character1Id = 3, Character2Id = 7, Character3Id = 11, Character4Id = 15, Character5Id = 23, Name = "Zephyr Champions", Score = 145 },
                new Team { Id = 9, UserId = 3, Character1Id = 7, Character2Id = 11, Character3Id = 15, Character4Id = 19, Character5Id = 23, Name = "Sky Protectors", Score = 140 },

                // Teams for User 4
                new Team { Id = 10, UserId = 4, Character1Id = 4, Character2Id = 8, Character3Id = 12, Character4Id = 16, Character5Id = 20, Name = "Earth Titans", Score = 160 },
                new Team { Id = 11, UserId = 4, Character1Id = 4, Character2Id = 8, Character3Id = 12, Character4Id = 16, Character5Id = 24, Name = "Rock Guardians", Score = 150 },
                new Team { Id = 12, UserId = 4, Character1Id = 8, Character2Id = 12, Character3Id = 16, Character4Id = 20, Character5Id = 24, Name = "Stone Warriors", Score = 140 }
            );
        }
    }
}
