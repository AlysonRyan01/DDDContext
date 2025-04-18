using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreateCreditCardSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string LastTransactionNumber { get; set; } = string.Empty;
        public string PaymentNumber { get; set; } = string.Empty;
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; } = string.Empty;
        public string PayerDocument { get; set; } = string.Empty;
        public string PayerEmail { get; set; } = string.Empty;
        public EDocumentType PayerDocumentType { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract<CreateCreditCardSubscriptionCommand>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "FirstName", "Nome é obrigatório")
                .IsNotNullOrEmpty(LastName, "LastName", "Sobrenome é obrigatório")

                // Documento e Email
                .IsNotNullOrEmpty(Document, "Document", "Documento é obrigatório")
                .IsNotNullOrEmpty(Email, "Email", "Email é obrigatório")
                .IsEmail(Email, "Email", "Email inválido")

                // Cartão
                .IsNotNullOrEmpty(CardHolderName, "CardHolderName", "Nome do titular do cartão é obrigatório")
                .IsNotNullOrEmpty(CardNumber, "CardNumber", "Número do cartão é obrigatório")
                .IsNotNullOrEmpty(LastTransactionNumber, "LastTransactionNumber", "Último número de transação é obrigatório")

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