using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CombatGameSite.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    DefenseMultiplier = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Attack", "Cost", "DefenseMultiplier", "Description", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, 0, 10, 1.2, "A soothing wave of water heals the target.", "Healing Wave", 1 },
                    { 2, 20, 15, 1.0, "A blazing sphere of fire strikes the enemy.", "Fireball", 2 },
                    { 3, 0, 12, 1.5, "A swirling wind protects against attacks.", "Wind Barrier", 3 },
                    { 4, 25, 18, 1.0, "A fiery explosion dealing massive damage.", "Inferno Burst", 2 },
                    { 5, 0, 8, 1.1000000000000001, "A small burst of water to heal minor wounds.", "Aqua Surge", 1 },
                    { 6, 0, 14, 1.8, "Earth magic hardens the skin for defense.", "Stone Skin", 4 },
                    { 7, 22, 20, 1.0, "A cutting wind attack that pierces defenses.", "Cyclone Slash", 3 },
                    { 8, 0, 9, 1.3999999999999999, "Encases the target in a veil of water for protection.", "Water Veil", 1 },
                    { 9, 0, 11, 1.3, "A gentle wind revitalizes the target.", "Breeze of Life", 3 },
                    { 10, 24, 17, 1.0, "An earth-shaking attack to crush enemies.", "Earthquake Smash", 4 },
                    { 11, 0, 16, 1.6000000000000001, "A shield of fire reduces incoming damage.", "Flame Ward", 2 },
                    { 12, 0, 13, 1.2, "The power of earth heals the target slowly.", "Earthen Rejuvenation", 4 },
                    { 13, 26, 19, 1.0, "A wave crashes on the enemy for damage.", "Tidal Crush", 1 },
                    { 14, 0, 10, 1.7, "A massive wind barrier blocks attacks.", "Wind Fortress", 3 },
                    { 15, 0, 12, 1.1000000000000001, "A fiery warmth restores the target's health.", "Flare Heal", 2 },
                    { 16, 28, 21, 1.0, "Rocks pummel the enemy, causing severe damage.", "Landslide Strike", 4 },
                    { 17, 0, 15, 1.5, "A shield of water dampens damage.", "Aqua Shield", 1 },
                    { 18, 0, 10, 1.1000000000000001, "A calming wind that heals minor injuries.", "Soothing Breeze", 3 },
                    { 19, 30, 22, 1.0, "A sword engulfed in fire slashes the enemy.", "Flaming Blade", 2 },
                    { 20, 0, 13, 1.8999999999999999, "A massive wall of earth defends against attacks.", "Rock Wall", 4 },
                    { 21, 0, 14, 1.2, "Tiny stones infused with healing power.", "Healing Pebbles", 4 },
                    { 22, 32, 25, 1.0, "A sharp blade of water cuts through the enemy.", "Aqua Slash", 1 },
                    { 23, 0, 18, 1.7, "A burning shield reduces damage taken.", "Blazing Barrier", 2 },
                    { 24, 0, 12, 1.2, "A soft breeze heals and rejuvenates.", "Whispering Wind", 3 },
                    { 25, 33, 23, 1.0, "A ferocious wind slashes the enemy.", "Tempest Fang", 3 },
                    { 26, 0, 15, 1.6000000000000001, "A watery shield protects the user.", "Ocean Shield", 1 },
                    { 27, 0, 10, 1.1000000000000001, "A quick burst of fire heals the target.", "Flame Pulse", 2 },
                    { 28, 34, 24, 1.0, "A giant boulder crushes the opponent.", "Boulder Bash", 4 },
                    { 29, 0, 19, 1.8, "A rotating wind reduces damage.", "Cyclone Guard", 3 },
                    { 30, 0, 14, 1.3, "A gentle rain restores the target's health.", "Rain of Healing", 1 },
                    { 31, 35, 26, 1.0, "A fiery wave burns all in its path.", "Flame Surge", 2 },
                    { 32, 0, 20, 1.8, "Earth barriers defend against attacks.", "Rock Shield", 4 },
                    { 33, 0, 13, 1.3, "A rejuvenating wind heals all nearby allies.", "Winds of Renewal", 3 },
                    { 34, 36, 27, 1.0, "A torrent of water smashes the enemy.", "Ocean Blast", 1 },
                    { 35, 0, 18, 1.7, "A barrier of molten fire absorbs damage.", "Lava Shield", 2 },
                    { 36, 0, 16, 1.2, "The earth's power restores health gradually.", "Earth Embrace", 4 },
                    { 37, 37, 29, 1.0, "A devastating lightning-charged wind attack.", "Storm Strike", 3 },
                    { 38, 0, 20, 1.6000000000000001, "Encases the user in a protective bubble.", "Bubble Shield", 1 },
                    { 39, 0, 12, 1.1000000000000001, "A healing ember that soothes wounds.", "Ember Heal", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Tagline" },
                values: new object[,]
                {
                    { 1, "alice", "password", "Hello, world!" },
                    { 2, "bob", "password", "Catchphrase." },
                    { 3, "charlie", "password", "Super smashing success." },
                    { 4, "dave", "password", "Game on." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
