namespace Entities.LinkModels
{
    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public Link() { }
        
        public Link(string method, string rel, string href)
        {
            Method = method;
            Rel = rel;
            Href = href;
        }
    }
}