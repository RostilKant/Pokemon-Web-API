namespace Entities.Models
{
    public class PokemonType
    {
        public int Id { get; set; }
        public Pokemon Pokemon { get; set; }

        public string Name { get; set; }
        public Type Type { get; set; }
    }
}