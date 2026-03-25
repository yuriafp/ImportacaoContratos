using ImportacaoContratos.BlazorUI.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace ImportacaoContratos.BlazorUI.Services;

public class RelatoriosService
{
    private readonly HttpClient _http;

    public RelatoriosService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ClienteResumoDto>> ObterResumoClientesAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ClienteResumoDto>>("api/relatorios/clientes-resumo");
        return result ?? new List<ClienteResumoDto>();
    }
    public async Task<List<string>> ImportarCsvAsync(IBrowserFile arquivo)
    {
        var content = new MultipartFormDataContent();
        long maxFileSize = 512 * 1024 * 1024; // 512MB

        var conteudoArquivo = new StreamContent(arquivo.OpenReadStream(maxFileSize));

        conteudoArquivo.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");

        content.Add(conteudoArquivo, "arquivo", arquivo.Name);

        var response = await _http.PostAsync("api/contratos/importar", content);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var listaDeErros = await response.Content.ReadFromJsonAsync<List<string>>();
                return listaDeErros ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        var errorBody = await response.Content.ReadAsStringAsync();

        throw new Exception(string.IsNullOrWhiteSpace(errorBody) ? "Erro:" : errorBody);
    }

    public async Task<List<ImportacaoDto>> ObterHistoricoImportacoesAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<List<ImportacaoDto>>("api/relatorios/importacoes")
                   ?? new List<ImportacaoDto>();
        }
        catch
        {
            return new List<ImportacaoDto>();
        }
    }
}