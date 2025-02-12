namespace BookStore.API.models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public double PriceInSYR { get; set; }
    }
}
