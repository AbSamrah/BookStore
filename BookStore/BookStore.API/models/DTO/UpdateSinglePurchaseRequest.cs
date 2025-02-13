namespace BookStore.API.models.DTO
{
    public class UpdateSinglePurchaseRequest
    {
        public int Quantity { get; set; }
        public Guid BookId { get; set; }
        public Guid PurchaseId { get; set; }
    }
}
