using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string TransactionCode { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public string PayerEmail { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreatePayPalSubscriptionCommand>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "FirstName", "Nome é obrigatório")
                .IsNotNullOrEmpty(LastName, "LastName", "Sobrenome é obrigatório")

                // Documento e Email
                .IsNotNullOrEmpty(Document, "Document", "Documento é obrigatório")
                .IsNotNullOrEmpty(Email, "Email", "Email é obrigatório")
                .IsEmail(Email, "Email", "Email inválido")

                // PayPal
                .IsNotNullOrEmpty(TransactionCode, "TransactionCode", "Código da transação é obrigatório")

                // Pagamento
                .IsNotNullOrEmpty(PaymentNumber, "PaymentNumber", "Número do pagamento é obrigatório")
                .IsGreaterThan(Total, 0, "Total", "Total deve ser maior que zero")
                .IsGreaterThan(TotalPaid, 0, "TotalPaid", "Total pago deve ser maior que zero")
                .IsLowerOrEqualsThan(PaidDate, DateTime.Now, "PaidDate", "A data de pagamento não pode ser futura")
                .IsGreaterThan(ExpireDate, DateTime.Now, "ExpireDate", "A data de expiração deve ser futura")

                // Pagador
                .IsNotNullOrEmpty(Payer, "Payer", "Pagador é obrigatório")
                .IsNotNullOrEmpty(PayerDocument, "PayerDocument", "Documento do pagador é obrigatório")
                .IsNotNullOrEmpty(PayerEmail, "PayerEmail", "Email do pagador é obrigatório")
                .IsEmail(PayerEmail, "PayerEmail", "Email do pagador inválido")

                // Endereço
                .IsNotNullOrEmpty(Street, "Street", "Rua é obrigatória")
                .IsNotNullOrEmpty(Number, "Number", "Número é obrigatório")
                .IsNotNullOrEmpty(Neighborhood, "Neighborhood", "Bairro é obrigatório")
                .IsNotNullOrEmpty(City, "City", "Cidade é obrigatória")
                .IsNotNullOrEmpty(State, "State", "Estado é obrigatório")
                .IsNotNullOrEmpty(Country, "Country", "País é obrigatório")
                .IsNotNullOrEmpty(ZipCode, "ZipCode", "CEP é obrigatório")
            );
        }
    }
}