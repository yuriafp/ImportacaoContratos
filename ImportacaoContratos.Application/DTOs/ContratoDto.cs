namespace ImportacaoContratos.Application.DTOs;

public class ContratoDto
{
    public string NumeroContrato { get; set; } = string.Empty;
    public string Produto { get; set; } = string.Empty;
    public DateTime Vencimento { get; set; }
    public decimal Valor { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string CpfCliente { get; set; } = string.Empty;
}