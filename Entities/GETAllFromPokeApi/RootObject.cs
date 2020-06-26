using System.Collections.Generic;

namespace Entities.GETAllFromPokeApi
{
    public class RootObject
    {
        public int Count { get; set; }
        public List<Result> Results { get; set; }
        
    }
}