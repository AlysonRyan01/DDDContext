using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void DeveRetornarErroQuandoCnpjForInvalido()
        {
            var doc = new Document("123", EDocumentType.Cnpj);
            Assert.IsFalse(doc.IsValid);
        }
        
        [TestMethod]
        public void DeveRetornarSucessoQuandoCnpjForValido()
        {
            var doc = new Document("12311111111111", EDocumentType.Cnpj);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void DeveRetornarErroQuandoCpfForInvalido()
        {
            var doc = new Document("102", EDocumentType.Cpf);
            Assert.IsFalse(doc.IsValid);
        }

        [TestMethod]
        public void DeveRetornarSucessoQuandoCpfForValido()
        {
            var doc = new Document("10266384027", EDocumentType.Cpf);
            Assert.IsTrue(doc.IsValid);
        }
    }
}