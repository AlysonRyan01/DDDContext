using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        var name = new Name("name", "teste");
    }
}
