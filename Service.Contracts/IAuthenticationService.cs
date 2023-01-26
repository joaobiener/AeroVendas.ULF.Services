using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using System.Security.Claims;

namespace Service.Contracts;

public interface IAuthenticationService
{
	Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
	Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
	Task<bool> ValidateUserLDAP(UserForAuthenticationDto userForAuth);
	Task<TokenDto> CreateToken(bool populateExp);	string GenerateRefreshToken();	ClaimsPrincipal GetPrincipalFromExpiredToken(string token);	Task<TokenDto> RefreshToken(TokenDto tokenDto);

}
