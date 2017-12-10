namespace Cash.Domain.Requests
{
    public class CreateAccountTransactionBalanceRequest : CreateAccountTransactionRequest
    {
        public decimal PreCreditAccountBalance { get; set; }

        public decimal PreDebitAccountBalance { get; set; }

        public decimal PostCreditAccountBalance { get; set; }

        public decimal PostDebitAccountBalance { get; set; }
    }
}