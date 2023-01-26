using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public class UserForRegistrationDto
{
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	
	[Required(ErrorMessage = "Username é obrigatório")]
	public string? UserName { get; set; }

	[Required(ErrorMessage = "Password é obrigatório")]
	public string? Password { get; set; }  = "Password1000";

	[Required(ErrorMessage = "Email é obrigatório")]
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	//public ICollection<string>? Roles { get; set; }
}
