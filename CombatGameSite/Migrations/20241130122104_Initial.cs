using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CombatGameSite.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack = table.Column<int>(type: "int", nullable: true),
                    DefenseMultiplier = table.Column<double>(type: "float", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillPrimaryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SkillSecondaryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SkillTertiaryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Skills_SkillPrimaryId",
                        column: x => x.SkillPrimaryId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Skills_SkillSecondaryId",
                        column: x => x.SkillSecondaryId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Skills_SkillTertiaryId",
                        column: x => x.SkillTertiaryId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    Character1Id = table.Column<int>(type: "int", nullable: true),
                    Character2Id = table.Column<int>(type: "int", nullable: true),
                    Character3Id = table.Column<int>(type: "int", nullable: true),
                    Character4Id = table.Column<int>(type: "int", nullable: true),
                    Character5Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Characters_Character1Id",
                        column: x => x.Character1Id,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Characters_Character2Id",
                        column: x => x.Character2Id,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Characters_Character3Id",
                        column: x => x.Character3Id,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Characters_Character4Id",
                        column: x => x.Character4Id,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Characters_Character5Id",
                        column: x => x.Character5Id,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Attack", "Cost", "DefenseMultiplier", "Description", "Name", "TypeId" },
                values: new object[,]
                {
                    { "A1", 20, 15, 1.0, "A blazing sphere of fire strikes the enemy.", "Fireball", 2 },
                    { "A10", 34, 24, 1.0, "A giant boulder crushes the opponent.", "Boulder Bash", 4 },
                    { "A11", 35, 26, 1.0, "A fiery wave burns all in its path.", "Flame Surge", 2 },
                    { "A12", 36, 27, 1.0, "A torrent of water smashes the enemy.", "Ocean Blast", 1 },
                    { "A13", 37, 29, 1.0, "A devastating lightning-charged wind attack.", "Storm Strike", 3 },
                    { "A2", 25, 18, 1.0, "A fiery explosion dealing massive damage.", "Inferno Burst", 2 },
                    { "A3", 22, 20, 1.0, "A cutting wind attack that pierces defenses.", "Cyclone Slash", 3 },
                    { "A4", 24, 17, 1.0, "An earth-shaking attack to crush enemies.", "Earthquake Smash", 4 },
                    { "A5", 26, 19, 1.0, "A wave crashes on the enemy for damage.", "Tidal Crush", 1 },
                    { "A6", 28, 21, 1.0, "Rocks pummel the enemy, causing severe damage.", "Landslide Strike", 4 },
                    { "A7", 30, 22, 1.0, "A sword engulfed in fire slashes the enemy.", "Flaming Blade", 2 },
                    { "A8", 32, 25, 1.0, "A sharp blade of water cuts through the enemy.", "Aqua Slash", 1 },
                    { "A9", 33, 23, 1.0, "A ferocious wind slashes the enemy.", "Tempest Fang", 3 },
                    { "D1", 0, 12, 1.5, "A swirling wind protects against attacks.", "Wind Barrier", 3 },
                    { "D10", 0, 19, 1.8, "A rotating wind reduces damage.", "Cyclone Guard", 3 },
                    { "D11", 0, 20, 1.8, "Earth barriers defend against attacks.", "Rock Shield", 4 },
                    { "D12", 0, 18, 1.7, "A barrier of molten fire absorbs damage.", "Lava Shield", 2 },
                    { "D13", 0, 20, 1.6000000000000001, "Encases the user in a protective bubble.", "Bubble Shield", 1 },
                    { "D2", 0, 14, 1.8, "Earth magic hardens the skin for defense.", "Stone Skin", 4 },
                    { "D3", 0, 9, 1.3999999999999999, "Encases the target in a veil of water for protection.", "Water Veil", 1 },
                    { "D4", 0, 16, 1.6000000000000001, "A shield of fire reduces incoming damage.", "Flame Ward", 2 },
                    { "D5", 0, 10, 1.7, "A massive wind barrier blocks attacks.", "Wind Fortress", 3 },
                    { "D6", 0, 15, 1.5, "A shield of water dampens damage.", "Aqua Shield", 1 },
                    { "D7", 0, 13, 1.8999999999999999, "A massive wall of earth defends against attacks.", "Rock Wall", 4 },
                    { "D8", 0, 18, 1.7, "A burning shield reduces damage taken.", "Blazing Barrier", 2 },
                    { "D9", 0, 15, 1.6000000000000001, "A watery shield protects the user.", "Ocean Shield", 1 },
                    { "H1", 0, 10, 1.2, "A soothing wave of water heals the target.", "Healing Wave", 1 },
                    { "H10", 0, 14, 1.3, "A gentle rain restores the target's health.", "Rain of Healing", 1 },
                    { "H11", 0, 13, 1.3, "A rejuvenating wind heals all nearby allies.", "Winds of Renewal", 3 },
                    { "H12", 0, 16, 1.2, "The earth's power restores health gradually.", "Earth Embrace", 4 },
                    { "H13", 0, 12, 1.1000000000000001, "A healing ember that soothes wounds.", "Ember Heal", 2 },
                    { "H2", 0, 8, 1.1000000000000001, "A small burst of water to heal minor wounds.", "Aqua Surge", 1 },
                    { "H3", 0, 11, 1.3, "A gentle wind revitalizes the target.", "Breeze of Life", 3 },
                    { "H4", 0, 13, 1.2, "The power of earth heals the target slowly.", "Earthen Rejuvenation", 4 },
                    { "H5", 0, 12, 1.1000000000000001, "A fiery warmth restores the target's health.", "Flare Heal", 2 },
                    { "H6", 0, 10, 1.1000000000000001, "A calming wind that heals minor injuries.", "Soothing Breeze", 3 },
                    { "H7", 0, 14, 1.2, "Tiny stones infused with healing power.", "Healing Pebbles", 4 },
                    { "H8", 0, 12, 1.2, "A soft breeze heals and rejuvenates.", "Whispering Wind", 3 },
                    { "H9", 0, 10, 1.1000000000000001, "A quick burst of fire heals the target.", "Flame Pulse", 2 }
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

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Defense", "Health", "Name", "SkillPrimaryId", "SkillSecondaryId", "SkillTertiaryId", "Species", "TypeId", "UserId" },
                values: new object[,]
                {
                    { 1, 50, 50, "Aqua Warrior", "A1", "A2", "H1", "Human", 2, 1 },
                    { 2, 50, 50, "Flame Destroyer", "A3", "A4", "H1", "Elf", 4, 2 },
                    { 3, 50, 50, "Wind Ninja", "A5", "A6", "H1", "Dwarf", 4, 3 },
                    { 4, 50, 50, "Earth Titan", "A7", "A8", "H1", "Human", 2, 4 },
                    { 5, 50, 50, "Water Sage", "A9", "A10", "H1", "Elf", 1, 1 },
                    { 6, 50, 50, "Fire Knight", "A11", "A12", "H1", "Dwarf", 3, 2 },
                    { 7, 50, 50, "Wind Shaman", "A13", "A13", "H1", "Human", 2, 3 },
                    { 8, 50, 50, "Earth Wizard", "H1", "H1", "H1", "Elf", 1, 4 },
                    { 9, 50, 50, "Water Spirit", "H1", "H1", "H1", "Dwarf", 4, 1 },
                    { 10, 50, 50, "Flame Phoenix", "H1", "H1", "H1", "Human", 2, 2 },
                    { 11, 50, 50, "Wind Warrior", "A1", "A2", "H2", "Elf", 3, 3 },
                    { 12, 50, 50, "Fire Sorcerer", "A3", "A4", "H2", "Dwarf", 4, 4 },
                    { 13, 50, 50, "Wind Elf", "A5", "A6", "H2", "Human", 2, 1 },
                    { 14, 50, 50, "Earth Golem", "A7", "A8", "H2", "Elf", 4, 2 },
                    { 15, 50, 50, "Water Knight", "A9", "A10", "H2", "Dwarf", 2, 3 },
                    { 16, 50, 50, "Fire Wizard", "A11", "A12", "H2", "Human", 2, 4 },
                    { 17, 50, 50, "Wind Shaman", "A13", "A13", "H2", "Elf", 3, 1 },
                    { 18, 50, 50, "Water Guardian", "H1", "H1", "H2", "Dwarf", 2, 2 },
                    { 19, 50, 50, "Flame Demon", "H1", "H1", "H2", "Human", 1, 3 },
                    { 20, 50, 50, "Wind Demon", "H1", "H1", "H2", "Elf", 2, 4 },
                    { 21, 50, 50, "Water Monk", "A1", "A2", "H3", "Dwarf", 2, 1 },
                    { 22, 50, 50, "Fire Samurai", "A3", "A4", "H3", "Human", 1, 2 },
                    { 23, 50, 50, "Wind Warrior", "A5", "A6", "H3", "Elf", 3, 3 },
                    { 24, 50, 50, "Earth Beast", "A7", "A8", "H3", "Dwarf", 1, 4 },
                    { 25, 50, 50, "Water Sorceress", "A9", "A10", "H3", "Human", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Character1Id", "Character2Id", "Character3Id", "Character4Id", "Character5Id", "Name", "Score", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 5, 9, 13, 17, "Aqua Defenders", 150, 1 },
                    { 2, 1, 5, 9, 13, 21, "Tidal Guardians", 140, 1 },
                    { 3, 9, 13, 17, 21, 25, "Water Warriors", 130, 1 },
                    { 4, 2, 6, 10, 14, 18, "Blazing Firestorm", 160, 2 },
                    { 5, 2, 6, 10, 14, 22, "Inferno Knights", 140, 2 },
                    { 6, 6, 10, 14, 18, 22, "Fire Warriors", 135, 2 },
                    { 7, 3, 7, 11, 15, 19, "Wind Strikers", 150, 3 },
                    { 8, 3, 7, 11, 15, 23, "Zephyr Champions", 145, 3 },
                    { 9, 7, 11, 15, 19, 23, "Sky Protectors", 140, 3 },
                    { 10, 4, 8, 12, 16, 20, "Earth Titans", 160, 4 },
                    { 11, 4, 8, 12, 16, 24, "Rock Guardians", 150, 4 },
                    { 12, 8, 12, 16, 20, 24, "Stone Warriors", 140, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SkillPrimaryId",
                table: "Characters",
                column: "SkillPrimaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SkillSecondaryId",
                table: "Characters",
                column: "SkillSecondaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SkillTertiaryId",
                table: "Characters",
                column: "SkillTertiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Character1Id",
                table: "Teams",
                column: "Character1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Character2Id",
                table: "Teams",
                column: "Character2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Character3Id",
                table: "Teams",
                column: "Character3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Character4Id",
                table: "Teams",
                column: "Character4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Character5Id",
                table: "Teams",
                column: "Character5Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
