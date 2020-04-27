using System.Collections.Generic;

namespace Entities.JsonModels
{
    //New RootObject for mapping with proper Url
    public class NewRootObject
    {
        public int Count { get; set; }
        public List<NResult> Results { get; set; }
    }
}