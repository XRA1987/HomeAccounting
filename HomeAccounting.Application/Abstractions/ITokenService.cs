using System.Security.Claims;

namespace HomeAccounting.Application.Abstractions
{
    public interface ITokenService
    {
        string GetAccessToken(Claim[] claims);
    }
}
