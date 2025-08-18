/*
    Domain/ - слой
    Users/ - Bounded Context
    Aggregates/ - Aggregates
    User/ - Aggregate
    User.cs - Root Aggregate (Через него идет взаимодействие с связанными объектамив агрегате User. Их пока что нет)
*/
using Domain.Users.ValueObjects; 

namespace Domain.Users.Aggregates.User;

public class User
{
    public Guid Id { get; init; }
    public Guid AccountId { get; init; }
    public string Name { get; private set; }
    public Address? Address { get; private set; }

    // Контролирует свои инвариаты
    public User(Guid id, Guid accountId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name);

        Id = id;
        AccountId = accountId;
        Name = name;
    }

    // Контролирует свои инвариаты
    public void SetName(string name)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name);

        Name = name;
    }

    // Контролирует свои инвариаты
    public void SetAddress(Address address)
    {
        Address = address;
    }
}