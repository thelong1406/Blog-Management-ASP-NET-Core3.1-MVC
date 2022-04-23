using System;

namespace DataAccess.Domain
{
    public class Cart
    {
        public long CartId { get; set; }
        public string Id { get; set; }
        public long ProductId { get; set; }
        public byte Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsPaid { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
