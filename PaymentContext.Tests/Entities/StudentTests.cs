using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Student _student;
    private readonly Subscription _subscription;
    private readonly Payment _payment;

    public StudentTests()
    {
        _student = new Student(new Name("Alyson", "Ullirsch"), new Document("10266384960", EDocumentType.Cpf), new Email("alyson@gmail.com"));
        _payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, new Document("10266384960", EDocumentType.Cpf),"Alyson", new Address("rua a", "500", "Centro", "Campo Largo", "Parana", "Brasil", "83601000"), new Email("alyson@gmail.com"), "23132132131");
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void DeveRetornarErroQuandoTiverUmaAssinaturaAtiva()
    {
        _subscription.AddPayment(_payment);

        var subscription2 = new Subscription(null);
        subscription2.AddPayment(_payment);

        _student.AddSubscription(_subscription);
        _student.AddSubscription(subscription2);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void DeveRetornarErroQuandoAssinaturaNaoTiverPagamento()
    {

        var subscription2 = new Subscription(null);
        _student.AddSubscription(subscription2);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void DeveRetornarSucessoQuandoNaoTiverUmaAssinaturaAtiva()
    {
        var subscription2 = new Subscription(null);
        subscription2.AddPayment(_payment);
        _student.AddSubscription(subscription2);

        Assert.IsTrue(_student.IsValid);
    }

    [TestMethod]
    public void DeveRetornarSucessoQuandoAssinaturaTiverPagamento()
    {

        var subscription2 = new Subscription(null);
        subscription2.AddPayment(_payment);
        _student.AddSubscription(subscription2);

        Assert.IsTrue(_student.IsValid);
    }
}