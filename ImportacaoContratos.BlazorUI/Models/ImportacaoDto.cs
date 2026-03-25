public class ImportacaoDto
{
    public int Id { get; set; }
    public string NomeArquivo { get; set; } = string.Empty;
    public DateTime DataImportacao { get; set; }
    public string NomeUsuario { get; set; } = string.Empty;
    public int TotalContratos { get; set; }
}