using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.Auth;

public interface IAuthService
{
    public string CreateToken(User user);
}
