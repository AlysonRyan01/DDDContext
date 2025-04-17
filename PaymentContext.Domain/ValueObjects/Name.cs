using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(FirstName, "FirstName", "O nome é obrigatório")
            .IsNotNullOrEmpty(LastName, "LastName", "O sobrenome é obrigatório")
        );
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}