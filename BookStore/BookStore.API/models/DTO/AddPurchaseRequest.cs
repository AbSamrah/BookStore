namespace BookStore.API.models.DTO
{
    public class AddPurchaseRequest
    {
        public IEnumerable<Book> Books { get; set; }
        public double GetTotalPrice()
        {
            double totalPrice = 0;
            foreach (var book in Books)
            {
                totalPrice += book.PriceInSYR;
            }
            return totalPrice;
        }
    }
}
