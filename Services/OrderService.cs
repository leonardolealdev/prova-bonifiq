using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			if (paymentMethod == "pix")
			{
				var paymentPix = new PaymentPix();
                paymentPix.DueDate = DateTime.Now;
                paymentPix.Key = "";
				//Faz pagamento...
			}
			else if (paymentMethod == "creditcard")
			{
                var paymentCredicard = new PaymentCredicard();
                paymentCredicard.DueDate = DateTime.Now;
                paymentCredicard.Number = "";
                //Faz pagamento...
            }
			else if (paymentMethod == "paypal")
			{
                var paymentPayPal = new PaymentPayPal();
                //Faz pagamento...
            }

			return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}
