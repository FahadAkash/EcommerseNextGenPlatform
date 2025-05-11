namespace EcommerseNextGenPlatform.Services.Interface
{
    public interface IBraintreeService
    {
        IBraintreeService CreateGateway();
        IBraintreeService GetGateway();
    }
}
