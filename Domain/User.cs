namespace Domain;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public Address? Address { get; private set; }

    public User(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name);

        Id = id;
        Name = name;
    }

    public void SetName(string name)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name);

        Name = name;
    }

    public void SetAddress(Address address)
    {
        Address = address;
    }
}