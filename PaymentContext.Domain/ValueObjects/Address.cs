using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;

        AddNotifications(new Contract<Address>()
            .Requires()
            .IsNotNullOrWhiteSpace(street, "A rua precisa ter pelo menos 1 caractere.")
            .IsNotNullOrWhiteSpace(number, "O número precisa ter pelo menos 1 caractere.")
            .IsNotNullOrWhiteSpace(neighborhood, "O bairro precisa ter pelo menos 1 caractere.")
            .IsNotNullOrWhiteSpace(city, "A cidade precisa ter pelo menos 1 caractere.")
            .IsNotNullOrWhiteSpace(state, "O estado precisa ter pelo menos 1 caractere.")
            .IsNotNullOrWhiteSpace(country, "O país precisa ter pelo menos 1 caractere.")
            .IsNotNullOrWhiteSpace(zipCode, "O CEP precisa ter pelo menos 1 caractere.")
        );
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
}