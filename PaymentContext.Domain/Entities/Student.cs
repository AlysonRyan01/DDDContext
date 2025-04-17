using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    private readonly IList<Subscription> _subscriptions;
    
    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();
        
        AddNotifications(name, document, email);
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address? Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get {return _subscriptions.ToArray(); } }

    public void AddSubscription(Subscription subscription)
    {        
        var hasSubscriptionActive = _subscriptions.Any(sub => sub.Active);

        AddNotifications(new Contract<Student>()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voce já tem uma assinatura ativa")
            .AreNotEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Essa assinatura não possui pagamentos")
            );

        if (IsValid)
            _subscriptions.Add(subscription);
    }
}