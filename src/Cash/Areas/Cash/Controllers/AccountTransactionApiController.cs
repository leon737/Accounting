using System.Web.Http;
using AutoMapper;
using Cash.Domain.Requests;
using Cash.Web.Areas.Cash.Models;

namespace Cash.Web.Areas.Cash.Controllers
{
    [Authorize]
    public class AccountTransactionApiController : ApiController
    {
        private readonly IMapper _mapper;

        public AccountTransactionApiController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [Route("api/transaction/create")]
        [HttpPost]
        public CreateTransactionResponse Create(CreateTransactionRequest request)
        {
            var serviceRequest = _mapper.Map<CreateAccountTransactionRequest>(request);

            return new CreateTransactionResponse
            {
                Status = CreateTransactionStatus.Faiure,
                Error = CreateTransactionError.CreditAndDebitAccountsAreSame
            };
        }

    }
}
