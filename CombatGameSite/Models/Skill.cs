namespace CombatGameSite.Models
{
    public class Skill
    {
        public string? Id { get; set; }
        public int? Cost { get; set; }
        public int? TypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Attack { get; set; }
        public double? DefenseMultiplier { get; set; }
    }
}
