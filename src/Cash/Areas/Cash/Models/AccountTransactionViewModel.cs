using System;

namespace Cash.Web.Areas.Cash.Models
{
    public class AccountTransactionViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal PreCreditAccountBalance { get; set; }

        public decimal PreDebitAccountBalance { get; set; }

        public decimal PostCreditAccountBalance { get; set; }

        public decimal PostDebitAccountBalance { get; set; }

        public decimal CurrencyRate { get; set; }

        public string Remark { get; set; }

        public Guid CreditAccountId { get; set; }

        public Guid DebitAccountId { get; set; }

    }
}