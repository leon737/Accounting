using System;

namespace Cash.Domain.Requests
{
    public class CreateAccountTransactionRequest
    {
        public Guid CreditAccountId { get; set; }

        public Guid DebitAccountId { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal DebitAmount { get; set; }
    
        public decimal CurrencyRate { get; set; }

        public string Remark { get; set; }

        public DateTime Date { get; set; }
    }
}