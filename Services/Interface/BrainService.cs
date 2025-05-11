using Braintree;

namespace EcommerseNextGenPlatform.Services.Interface
{
    public class BrainService : IBraintreeService
    {
        private readonly IConfiguration _configuration;
        public BrainService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IBraintreeService CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _configuration.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey = _configuration.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey = _configuration.GetValue<string>("BraintreeGateway:PrivateKey")
            };

            return (IBraintreeService)newGateway;

        }

        public IBraintreeService GetGateway()
        {
            return CreateGateway(); 
            
        }
    }
}
