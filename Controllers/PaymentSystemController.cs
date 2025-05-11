using Braintree;
using EcommerseNextGenPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerseNextGenPlatform.Controllers
{
    public class PaymentSystemController : ControllerBase
    {
        private readonly IBraintreeService _braintreeService;   

        public PaymentSystemController(IBraintreeService braintreeService)
        {
            _braintreeService = braintreeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var gateway = _braintreeService.GetGateway();
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal("250"),
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }

            };
            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                return (Ok());
            }
            else
            {
                return BadRequest("Transaction Have some Problem Please Fixed it ");
            }
        }

    }
}
