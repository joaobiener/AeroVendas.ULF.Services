using AeroVendas.ULF.Services.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace AeroVendas.ULF.Services.Controllers;

[Route("token")]
[ApiController]
public class TokenController : ControllerBase
{
	private readonly IServiceManager _service;

	public TokenController(IServiceManager service) => _service = service;

	[HttpPost("refresh")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
	{
		var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);

		return Ok(new AuthResponseDto{
			Token = tokenDtoToReturn.AccessToken,
			RefreshToken = tokenDtoToReturn.RefreshToken,
			IsAuthSuccessful = true
		});
	}
}
