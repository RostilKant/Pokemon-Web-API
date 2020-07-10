using System.Collections.Generic;

namespace Entities.LinkModels
{
    public class LinkResource
    {
        public LinkResource() { }
        
        public List<Link> Links { get; set; } = new List<Link>();
    }
}