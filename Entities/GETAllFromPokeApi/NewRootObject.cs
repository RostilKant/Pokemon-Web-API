using System.Collections.Generic;
using Entities.JsonModels;

namespace Entities.GET_All_JsonModels
{
    //New RootObject for mapping with proper Url
    public class NewRootObject
    {
        public int Count { get; set; }
        public List<NResult> Results { get; set; }
    }
}