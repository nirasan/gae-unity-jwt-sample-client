public class RegistrationHandlerRequest
{
    public string Username;
    public string Password;
}
public class RegistrationHandlerResponse
{
    public bool Success;
}
public class AuthenticationHandlerRequest
{
    public string Username;
    public string Password;
}
public class AuthenticationHandlerResponse
{
    public bool Success;
}
public class HelloWorldHandlerResponse
{
    public bool Success;
    public string Message;
}
