namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        private const int MaxSize = 50;
        public int PageNumber { get; set; } = 1;
        
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxSize) ? _pageSize : value;
        }
        
        public string OrderBy { get;set;}
    }

    public class PokemonPageParameters : RequestParameters
    {
        public PokemonPageParameters()
        {
            OrderBy = "name";
        }
        public string Type { get; set; }
        public string Name { get; set; }

        
    }
}