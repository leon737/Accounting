namespace Cash.Web.Areas.Cash.Models
{
    public enum CreateTransactionError
    {
        NoError,

        CreditAndDebitAccountsAreSame,

        AmountIsZero,

        CreditAccountConstraintViolation,

        DebitAccountConstraintViolation,

        CreditAccountTimelineViolation,

        DebitAccountTimelineViolation
    }
}