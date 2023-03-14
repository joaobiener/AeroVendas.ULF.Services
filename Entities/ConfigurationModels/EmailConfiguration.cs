namespace Entities.ConfigurationModels;

public class EmailConfiguration
{
	public string Section { get; set; } = "EmailSettings";

	public string? UrlSitePaginaAceiteEmail { get; set; }
	public string? NameFrom { get; set; }
	public string? EmailFrom { get; set; }
	public string? SmtpHost { get; set; }
	public int SmtpPort { get; set; }
	public string? SmtpUser { get; set; }
	public string? SmtpPass { get; set; }

}
