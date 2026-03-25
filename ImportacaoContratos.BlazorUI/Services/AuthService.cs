using System.Net.Http.Json;
using Microsoft.JSInterop;
using ImportacaoContratos.BlazorUI.Models;

namespace ImportacaoContratos.BlazorUI.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", loginDto);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResult>();

            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", result?.Token);
            return true;
        }

        return false;
    }

    public async Task<string?> Registro(RegistroDto registroDto)
    {
        var response = await _http.PostAsJsonAsync("api/auth/registrar", registroDto);

        if (response.IsSuccessStatusCode)
            return null;

        var erro = await response.Content.ReadAsStringAsync();
        return erro ?? "Erro ao criar conta.";
    }
}