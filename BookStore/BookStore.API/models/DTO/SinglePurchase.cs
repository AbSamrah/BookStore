namespace BookStore.API.models.DTO
{
    public class SinglePurchase
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid BookId { get; set; }
        public Guid PurchaseId { get; set; }
    }
}
