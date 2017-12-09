using System;

namespace Cash.Web.Areas.Cash.Models
{
    public class CreateTransactionResponse
    {
        public CreateTransactionStatus Status { get; set; }

        public CreateTransactionError Error { get; set; }

        public Guid TransactionId { get; set; }
    }
}