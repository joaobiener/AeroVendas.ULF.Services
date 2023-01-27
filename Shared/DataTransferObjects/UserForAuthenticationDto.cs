using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record UserForAuthenticationDto
{
	[Required(ErrorMessage = "Login é obrigatório.")]
	public string? UserName { get; set; }
	[Required(ErrorMessage = "Password é obrigatória.")]
	public string? Password { get; set; } 
}
