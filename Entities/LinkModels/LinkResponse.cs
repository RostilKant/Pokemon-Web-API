using System.Collections.Generic;
using Entities.Models;

namespace Entities.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        public List<Entity> ShapedObjects { get; set; }
        public LinkCollectionWrapper<Entity> LinkedShapedObjects { get; set; }

        public LinkResponse()
        {
            LinkedShapedObjects = new LinkCollectionWrapper<Entity>();
            ShapedObjects = new List<Entity>();
        }
    }
}