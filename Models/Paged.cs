namespace ProvaPub.Models
{
    public abstract class Paged
    {
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
