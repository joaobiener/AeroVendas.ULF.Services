using AeroVendas.ULF.Services.Presentation.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace AeroVendas.ULF.Services.Presentation.Controllers;

[Route("account")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly IServiceManager _service;

	public AuthenticationController(IServiceManager service) => _service = service;

	[HttpPost("register")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	//[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
	{

		var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
	
		if (!result.Succeeded)
		{
			var errors = result.Errors.Select(e => e.Description);
			return BadRequest(new ResponseDto { Errors = errors });
			//foreach (var error in result.Errors)
			//{
			//	ModelState.TryAddModelError(error.Code, error.Description);
			//}
			return BadRequest(ModelState);
		}

		return StatusCode(201);
	}


	[HttpPost("login")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
	{
		if (!await _service.AuthenticationService.ValidateUser(user))
			return Unauthorized();

		var tokenDto = await _service.AuthenticationService
		.CreateToken(populateExp: true);
		return Ok(tokenDto);	}

	//Login de rede
	[HttpPost("loginLDAP")]
	[ServiceFilter(typeof(ValidationFilterAttribute))]
	public async Task<IActionResult> AuthenticateLDAP([FromBody] UserForAuthenticationDto user)
	{
		if (!await _service.AuthenticationService.ValidateUserLDAP(user))
			return Unauthorized(new AuthResponseDto
			{
				ErrorMessage = "Autenticação Inválida"
			}) ;

		var tokenDto = await _service.AuthenticationService
		.CreateToken(populateExp: true);
		return Ok(new AuthResponseDto
		{
			IsAuthSuccessful= true,
			Token = tokenDto.AccessToken,
			RefreshToken = tokenDto.RefreshToken

		}); 	}


}
