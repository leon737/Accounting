using System;
using System.Collections.Generic;

namespace Cash.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public AccountType AccountType { get; set; }

        public decimal Balance { get; set; }

        public virtual ICollection<AccountTransaction> Transactions { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedBy { get; set; }

        public virtual User CreatedByUser { get; set; }
    }
}