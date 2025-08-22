using Domain.Users.ValueObjects;

namespace Application.Users.DTOs;

public class UserDTO
{
    public Guid Id { get; init; }
    public Guid AccountId { get; init; }
    public string Name { get; init; }
    public Address? Address { get; init; }

    public UserDTO(Guid id, Guid accountId, string name, Address? address)
    {
        Id = id;
        AccountId = accountId;
        Name = name;
        Address = address;
    }
}