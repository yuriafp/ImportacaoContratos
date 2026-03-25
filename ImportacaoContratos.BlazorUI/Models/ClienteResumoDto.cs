namespace ImportacaoContratos.BlazorUI.Models;

public class ClienteResumoDto
{
    public string NomeCliente { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public decimal ValorTotalContratos { get; set; }
    public int MaiorAtrasoDias { get; set; }
    public List<ContratoResumoItemDto> Contratos { get; set; } = new();
}

public class ContratoResumoItemDto
{
    public string NumeroContrato { get; set; } = string.Empty;
    public string Produto { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Vencimento { get; set; }
    public int DiasAtraso { get; set; }
}