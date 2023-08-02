namespace prjMvcCoreDemo.Models
{
    public class CShoppingCartItem
    {
        public int productId { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }
        public decimal 小計 { get { return this.price * this.count; } }
        public TProduct product { get; set; }
    }
}
