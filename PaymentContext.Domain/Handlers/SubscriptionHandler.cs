using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler 
    : Notifiable<Notification>,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRespository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRespository respository, IEmailService emailService)
        {
            _repository = respository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Ocorreu um erro na validação do command");
            }

            if (_repository.DocumentExist(command.Document))
                AddNotification("Document", "Esse CPF já está sendo usado");

            if (_repository.EmailExist(command.Email))
                AddNotification("Email", "Esse email já está sendo usado");
            
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, new Document(command.PayerDocument, command.PayerDocumentType), command.Payer, address, email, command.BarCode, command.BoletoNumber);
            
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if (!IsValid)
                return new CommandResult(false, "Erro ao processar assinatura");

            _repository.CreateSubscription(student);

            _emailService.send(student.Name.FullName, student.Email.Address, "Bem vindo ao nosso site", "Sua assinatura foi criada");

            return new CommandResult(true, "Sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Ocorreu um erro na validação do command");
            }

            if (_repository.DocumentExist(command.Document))
                AddNotification("Document", "Esse CPF já está sendo usado");

            if (_repository.EmailExist(command.Email))
                AddNotification("Email", "Esse email já está sendo usado");
            
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, new Document(command.PayerDocument, command.PayerDocumentType), command.Payer, address, email, command.TransactionCode);
            
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if (!IsValid)
                return new CommandResult(false, "Erro ao processar assinatura");

            _repository.CreateSubscription(student);

            _emailService.send(student.Name.FullName, student.Email.Address, "Bem vindo ao nosso site", "Sua assinatura foi criada");

            return new CommandResult(true, "Sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Ocorreu um erro na validação do command");
            }

            if (_repository.DocumentExist(command.Document))
                AddNotification("Document", "Esse CPF já está sendo usado");

            if (_repository.EmailExist(command.Email))
                AddNotification("Email", "Esse email já está sendo usado");
            
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Enums.EDocumentType.Cpf);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, new Document(command.PayerDocument, command.PayerDocumentType), command.Payer, address, email, command.CardHolderName, command.CardNumber, command.LastTransactionNumber);
            
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if (!IsValid)
                return new CommandResult(false, "Erro ao processar assinatura");

            _repository.CreateSubscription(student);

            _emailService.send(student.Name.FullName, student.Email.Address, "Bem vindo ao nosso site", "Sua assinatura foi criada");

            return new CommandResult(true, "Sucesso");
        }
    }
}