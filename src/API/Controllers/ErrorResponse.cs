namespace API.Controllers;

public record class ErrorResponse(string Error, string Message, object Details); 