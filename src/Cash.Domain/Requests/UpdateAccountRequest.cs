using System;
using Cash.Domain.Models;

namespace Cash.Domain.Requests
{
    public class UpdateAccountRequest : UpdateAccountInfoRequest
    {
        public AccountType Type { get; set; }

        public Guid CurrencyId { get; set; }
        
    }
}