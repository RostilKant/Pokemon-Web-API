using System.Dynamic;

namespace Entities.Models
{
    public class ShapedExpObject
    {
        public int Id { get; set; }
        public ExpandoObject ExpandoObject { get; set; }
        
        public ShapedExpObject()
        {
            ExpandoObject = new ExpandoObject();
        }
    }
}