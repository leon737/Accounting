using System;

namespace Cash.Domain.Models
{
    public class AccountTransaction
    {

        public Guid Id { get; set; }

        public Guid Debit { get; set; }

        public virtual Account DebitAccount { get; set; }

        public Guid Credit { get; set; }

        public virtual Account CreditAccount { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public decimal DebitAccountBalance { get; set; }

        public decimal CreditAccountBalance { get; set; }

        public string Description { get; set; }
    }
}