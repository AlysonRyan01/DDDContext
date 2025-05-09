using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRespository
    {
        bool DocumentExist(string document);
        bool EmailExist(string email);
        void CreateSubscription(Student student);
    }
}