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
                    Health = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
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
                    { 1, 0, 100, "Aqua Warrior", "A1", "A2", "D1", "Human", 1, 1 },
                    { 2, 10, 95, "Flame Destroyer", "A3", "A4", "D3", "Elf", 2, 2 },
                    { 3, 0, 100, "Wind Ninja", "A5", "A6", "D2", "Ninja", 3, 3 },
                    { 4, 20, 100, "Earth Titan", "A7", "A8", "D4", "Giant", 4, 4 },
                    { 5, 10, 100, "Water Sage", "A9", "A10", "H1", "Mage", 1, 1 },
                    { 6, 15, 80, "Fire Knight", "A11", "A12", "D1", "Knight", 2, 2 },
                    { 7, 0, 100, "Wind Shaman", "A13", "A4", "H2", "Shaman", 3, 3 },
                    { 8, 25, 90, "Earth Wizard", "A7", "A6", "D2", "Wizard", 4, 4 },
                    { 9, 5, 85, "Water Spirit", "A7", "A8", "H3", "Spirit", 1, 1 },
                    { 10, 5, 100, "Flame Phoenix", "A9", "A2", "D3", "Phoenix", 2, 2 },
                    { 11, 10, 80, "Wind Warrior", "A2", "A2", "D4", "Warrior", 3, 3 },
                    { 12, 30, 95, "Earth Golem", "A2", "A4", "D1", "Golem", 4, 4 },
                    { 13, 20, 90, "Water Knight", "A5", "A6", "H4", "Knight", 1, 1 },
                    { 14, 5, 100, "Fire Sorcerer", "A7", "A8", "D2", "Sorcerer", 2, 2 },
                    { 15, 15, 80, "Wind Elf", "A9", "A3", "D3", "Elf", 3, 3 },
                    { 16, 35, 100, "Earth Monk", "A3", "A2", "D4", "Monk", 4, 4 },
                    { 17, 10, 90, "Water Guardian", "A3", "A4", "H5", "Guardian", 1, 1 },
                    { 18, 10, 85, "Fire Wizard", "A5", "A6", "D1", "Wizard", 2, 2 },
                    { 19, 5, 95, "Wind Dragon", "A7", "A8", "D2", "Dragon", 3, 3 },
                    { 20, 50, 100, "Earth Demon", "A9", "A4", "D3", "Demon", 4, 4 },
                    { 21, 5, 100, "Water Monk", "A1", "A2", "H6", "Monk", 1, 1 },
                    { 22, 15, 90, "Fire Samurai", "A3", "A4", "D4", "Samurai", 2, 2 },
                    { 23, 20, 80, "Wind Swordsman", "A5", "A6", "D1", "Swordsman", 3, 3 },
                    { 24, 40, 100, "Earth Beast", "A7", "A8", "D2", "Beast", 4, 4 },
                    { 25, 0, 90, "Water Sorceress", "A9", "A5", "D3", "Sorceress", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Character1Id", "Character2Id", "Character3Id", "Character4Id", "Character5Id", "Name", "Score", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 2, 3, 4, 5, "Water Warriors", 150, 1 },
                    { 2, 6, 7, 8, 9, 10, "Fire Phoenixes", 120, 2 },
                    { 3, 11, 12, 13, 14, 15, "Wind Striders", 140, 3 },
                    { 4, 16, 17, 18, 19, 20, "Earth Guardians", 160, 4 },
                    { 5, 21, 22, 23, 24, 25, "Aqua Knights", 130, 1 },
                    { 6, 2, 5, 7, 10, 6, "Inferno Lords", 110, 2 },
                    { 7, 3, 8, 12, 13, 11, "Storm Warriors", 135, 3 },
                    { 8, 14, 16, 17, 19, 20, "Quake Defenders", 125, 4 },
                    { 9, 22, 23, 1, 4, 5, "Tidal Fury", 145, 1 },
                    { 10, 9, 10, 6, 2, 7, "Blazing Souls", 115, 2 },
                    { 11, 13, 11, 8, 15, 12, "Whirlwind Vanguards", 150, 3 },
                    { 12, 18, 14, 19, 20, 16, "Rock Titans", 160, 4 }
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
