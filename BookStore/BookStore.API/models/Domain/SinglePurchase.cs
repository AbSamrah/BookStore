namespace BookStore.API.models.Domain
{
    public class SinglePurchase
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid BookId { get; set; }
        public Guid PurchaseId { get; set; }

        public Book Book { get; set; }
        public Purchase Purchase { get; set; }
    }
}
