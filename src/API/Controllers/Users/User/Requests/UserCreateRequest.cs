namespace API.Controllers.Users.User.Requests;

public class UserCreateRequest
{
    public string Name { get; init; }

    public UserCreateRequest(string name)
    {
        Name = name;
    }
}
