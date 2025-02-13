namespace BookStore.API.models.Domain
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public DateTime PurchaseTime { get; set; }
        public IEnumerable<SinglePurchase> SinglePurchases { get; set; }
    }
}
