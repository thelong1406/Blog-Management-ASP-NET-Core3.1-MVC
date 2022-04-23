using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double? OnSalePrice { get; set; }
        public byte[] Thumbnail { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
