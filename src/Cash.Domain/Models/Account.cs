using System;
using System.Collections.Generic;

namespace Cash.Domain.Models
{
    public class Account : EntityWithFullStatisticalData
    {
        public Guid Id { get; set; }

        public Guid ChartId { get; set; }

        public virtual Chart Chart { get; set; }

        public Guid? ParentAccountId { get; set; }

        public virtual Account ParentAccount { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public int Code { get; set; }

        public AccountType AccountType { get; set; }

        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public bool Locked { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public Guid? LastUpdatedBy { get; set; }

        public virtual User LastUpdateByUser { get; set; }

        public virtual ICollection<Account> ChildAccounts { get; set; }
    }
}