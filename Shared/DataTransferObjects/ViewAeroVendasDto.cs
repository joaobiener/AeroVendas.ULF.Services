using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DataTransferObjects;

public record ViewAeroVendasDto(string? Contrato, 
                                  string? CodigoBeneficiario, 
                                  string? NomeBeneficiario, 
                                  string? EmailBeneficiario,
                                  double? PremioAtual,
                                  string? Cidade,
                                  int? NumeroDependentes);

