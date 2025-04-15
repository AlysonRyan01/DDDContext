namespace PaymentContext.Domain.Entities;

public class PayPalPayment : Payment
{
    public PayPalPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string document, string payer, string address, string email, string transactionCode) 
        : base(paidDate, expireDate, total, totalPaid, document, payer, address, email)
    {
        TransactionCode = transactionCode;
    }

    public string TransactionCode { get; private set; }
}