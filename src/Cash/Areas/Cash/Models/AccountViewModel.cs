using System;
using Cash.Domain.Models;

namespace Cash.Web.Areas.Cash.Models
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public Guid? ParentAccountId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public AccountType Type { get; set; }

        public Guid CurrencyId { get; set; }

        public decimal Balance { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public string LastUpdatedBy { get; set; }

        public bool HasTransactions { get; set; }
    }
}