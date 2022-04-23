namespace DataAccess.Domain
{
    public class InvoiceProduct
    {
        public long ProductId { get; set; }
        public long InvoiceId { get; set; }
        public byte Quantity { get; set; }
        public double CurrentPice { get; set; }
        public Product Product { get; set; }
        public Invoice Invoice { get; set; }
    }
}
