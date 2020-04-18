using System.Collections.Generic;
using System.Xml;

namespace Entities.JsonModels
{
    public class RootObject
    {
        public int Count { get; set; }
        public List<Result> Results { get; set; }
        
    }
}