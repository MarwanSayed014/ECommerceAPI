namespace ECommerceAPI.BL.ViewModels
{
    public class CartViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public int Amount { get; set; }
    }
}
