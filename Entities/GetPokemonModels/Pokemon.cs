using System.Collections.Generic;

namespace Entities.GetPokemonModels
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        
        public List<PokemonType> Types { get; set; }
        //public List<EncountersArea> EncountersAreas { get; set; }
    }
}