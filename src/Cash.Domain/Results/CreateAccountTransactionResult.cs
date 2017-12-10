using System;

namespace Cash.Domain.Results
{
    public class CreateAccountTransactionResult
    {
        public CreateTransactionStatus Status { get; set; }

        public CreateTransactionError Error { get; set; }

        public Guid TransactionId { get; set; }

        public static CreateAccountTransactionResult Success(Guid transactionId)
        {
            return new CreateAccountTransactionResult
            {
                Status = CreateTransactionStatus.Success,
                Error = CreateTransactionError.NoError,
                TransactionId = transactionId
            };
        }

        public static CreateAccountTransactionResult Fail(CreateTransactionError error)
        {
            return new CreateAccountTransactionResult
            {
                Status = CreateTransactionStatus.Faiure,
                Error = error
            };
        }
    }
}
