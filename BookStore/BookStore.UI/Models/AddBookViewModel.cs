namespace BookStore.UI.Models
{
    public class AddBookViewModel
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double PriceInSYR { get; set; }
        public int Quantity { get; set; }
    }
}
