namespace Application.Domain.Shared;

public record Address : IValueObject
{
    public Address(string city, string zipCode, string street)
    {
        City = city;
        ZipCode = zipCode;
        Street = street;
    }

    public string City { get; private set; }
    public string ZipCode { get; private set; }
    public string Street { get; private set; }
}