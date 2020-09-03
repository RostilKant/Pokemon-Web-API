using System.Collections.Generic;

namespace Entities.GetPokemonsFromPokeApi
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<Sprite> Sprites { get; set; }
        
        public List<PokemonType> Types { get; set; }
    }
}