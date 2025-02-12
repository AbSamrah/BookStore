namespace BookStore.API.models.DTO
{
    public class AddBookRequest
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; }
        public double PriceInSYR { get; set; }
    }
}
