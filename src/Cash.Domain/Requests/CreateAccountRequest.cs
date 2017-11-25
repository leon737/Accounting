using System;

namespace Cash.Domain.Requests
{
    public class CreateAccountRequest : UpdateAccountRequest
    {
        public Guid ChartId { get; set; }

        public Guid? ParentAccountId { get; set; }
     
    }
}
