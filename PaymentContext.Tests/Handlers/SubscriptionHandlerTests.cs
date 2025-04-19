using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void DeveRetornarErroDocumentoExistente()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand
            {
                FirstName = "João",
                LastName = "Silva",
                Document = "99999999999", // Documento já existente no fake repository
                Email = "alyson@email.com",
                BarCode = "232938928392",
                BoletoNumber = "123456789",
                PaymentNumber = "12345",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(30),
                Total = 100,
                TotalPaid = 100,
                Payer = "João Silva",
                PayerDocument = "99999999999",
                PayerDocumentType = EDocumentType.Cpf,
                PayerEmail = "alyson@email.com",
                Street = "Rua A",
                Number = "123",
                Neighborhood = "Centro",
                City = "São Paulo",
                State = "SP",
                Country = "Brasil",
                ZipCode = "12345678"
            };

            command.Validate();
            Assert.IsTrue(command.IsValid); // O comando deve ser válido

            handler.Handle(command);

            Assert.IsFalse(handler.IsValid); // Esperamos que o handler retorne inválido porque o documento já existe
        }
    }
}