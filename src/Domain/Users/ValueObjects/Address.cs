namespace Domain.Users.ValueObjects;

public record class Address
{
    public string City { get; init; }

    private Address() {}

    public Address(string city)
    {
        City = city;
    }
}