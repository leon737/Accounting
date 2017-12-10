namespace Cash.Domain.Results
{
    public enum CreateTransactionError
    {
        NoError,

        CreditAndDebitAccountsAreSame,

        AmountIsZero,

        CreditAccountConstraintViolation,

        DebitAccountConstraintViolation,

        CreditAccountTimelineViolation,

        DebitAccountTimelineViolation,

        CreditAccountLocked,

        DebitAccountLocked
    }
}