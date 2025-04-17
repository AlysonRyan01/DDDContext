using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;
using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType documentType)
    {
        Number = number;
        DocumentType = documentType;

        AddNotifications(new Contract<Document>()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Documento inválido")
            );
    }

    public string Number { get; private set; }
    public EDocumentType DocumentType { get; private set; }

    private bool Validate()
    {
        if(DocumentType == EDocumentType.Cnpj && Number.Length == 14)
            return true;

        if (DocumentType == EDocumentType.Cpf && Number.Length == 11)
            return true;

        return false;
    }
}