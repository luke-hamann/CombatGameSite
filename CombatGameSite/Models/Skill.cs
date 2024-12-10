namespace CombatGameSite.Models
{ //Model for holding information about skills
    public class Skill
    {
        public string? Id { get; set; }
        public int? Cost { get; set; }
        public int? TypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Attack { get; set; }

        public new string GetType() =>
            TypeId switch
            {
                1 => "Water",
                2 => "Fire",
                3 => "Wind",
                4 => "Earth",
                _ => "",
            }; //Returns display text for TypeId.
    }
}
