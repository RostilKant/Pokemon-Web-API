namespace Entities
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
    }

    public class PokemonPageParameters : RequestParameters
    {
        public string Type { get; set; }
    }
}