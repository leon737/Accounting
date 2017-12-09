using System;

namespace Cash.Web.Areas.Cash.Models
{
    public class CreateTransactionRequest
    {
        public Guid CreditAccount { get; set; }

        public Guid DebitAccount { get; set; }

        public decimal Amount { get; set; }

        public long? Date { get; set; }

        public string Remark { get; set; }
    }
}