using System;

namespace Cash.Domain.Models
{
    public class AccountTransaction : EntityWithStatisticalData
    {
        public Guid Id { get; set; }

        public Guid CreditAccountId { get; set; }

        public virtual Account CreditAccount { get; set; }

        public Guid DebitAccountId { get; set; }

        public virtual Account DebitAccount { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAccountBalance { get; set; }

        public decimal DebitAccountBalance { get; set; }

        public decimal CurrencyRate { get; set; }

        public string Remark { get; set; }
        
    }
}