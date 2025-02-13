namespace BookStore.API.models.DTO
{
    public class AddSinglePurchaseRequest
    {
        public int Quantity { get; set; }
        public Guid BookId { get; set; }
        public Guid PurchaseId { get; set; }
    }
}
