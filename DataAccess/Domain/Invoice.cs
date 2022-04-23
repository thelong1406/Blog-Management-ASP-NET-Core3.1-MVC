using System;
using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime PaidDate { get; set; }
        public double TotalPrice { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
    }
}
