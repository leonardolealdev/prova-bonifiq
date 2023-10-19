namespace ProvaPub.Models
{
    public abstract class Payment 
    {
        public DateTime DueDate { get; set; }
    }

    public class PaymentPix : Payment
    {
        public string Key { get; set; }
    }

    public class PaymentCredicard : Payment
    {
        public string Number { get; set; }
    }

    public class PaymentPayPal : Payment
    {
        //dadps paypal
    }
}
