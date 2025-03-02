namespace BookStore.API.models.DTO
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
        public double PriceInSYR { get; set; }
        public int Quantity { get; set; }
    }
}
