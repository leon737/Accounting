using System;
using Cash.Domain.Models;

namespace Cash.Domain.Requests
{
    public class CreateAccountRequest : UpdateAccountInfoRequest
    {
        public Guid ChartId { get; set; }

        public Guid? ParentAccountId { get; set; }

        public AccountType AccountType { get; set; }

        public Guid CurrencyId { get; set; }
        
    }
}
