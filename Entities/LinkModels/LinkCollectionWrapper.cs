using System.Collections.Generic;

namespace Entities.LinkModels
{
    public class LinkCollectionWrapper<T> : LinkResource
    {
        public List<T> Value { get; set; }

        public LinkCollectionWrapper() { }
        
        public LinkCollectionWrapper(List<T> value)
        {
            Value = value;
        }
    }
}