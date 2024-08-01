using System.Net;

namespace MenuMasterAPI;

public class UserPasswordIncorrectException : BaseException
{
    public UserPasswordIncorrectException(string property) : base($"Wrong password for user with email {property}.", HttpStatusCode.Unauthorized)
    {

    }
}
