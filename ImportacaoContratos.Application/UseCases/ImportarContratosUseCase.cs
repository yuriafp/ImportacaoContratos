using ImportacaoContratos.Application.Services;
using ImportacaoContratos.Application.Validations;
using ImportacaoContratos.Domain.Entities;
using ImportacaoContratos.Domain.Interfaces;
using ImportacaoContratos.Application.Extensions; // <-- Não te esqueças deste using!

namespace ImportacaoContratos.Application.UseCases;

public class ImportarContratosUseCase
{
    private readonly IContratoRepository _repository;
    private readonly CsvReaderService _csvReader;

    public ImportarContratosUseCase(IContratoRepository repository, CsvReaderService csvReader)
    {
        _repository = repository;
        _csvReader = csvReader;
    }

    public async Task<List<string>> ExecutarAsync(Stream arquivoStream, string nomeArquivo, int usuarioId)
    {
        var registrosCsv = _csvReader.LerContratosDeCsv(arquivoStream);
        var validador = new ContratoCsvValidator();
        var listaDeErros = new List<string>();

        var importacao = new Importacao
        {
            NomeArquivo = nomeArquivo,
            DataImportacao = DateTime.Now,
            UsuarioId = usuarioId
        };

        int linhaAtual = 1;

        foreach (var registro in registrosCsv)
        {
            linhaAtual++; //levando em conta o cabeçalho do CSV

            var resultadoValidacao = validador.Validate(registro);

            if (!resultadoValidacao.IsValid)
            {
                var errosDaLinha = string.Join(" | ", resultadoValidacao.Errors.Select(e => e.ErrorMessage));
                listaDeErros.Add($"Erro na Linha {linhaAtual} (CPF: {registro.Cpf}): {errosDaLinha}");
                continue;
            }

            // Normalização dos dados: CPF só com números, e campos de texto sem acentos e espaços extras
            var cpfLimpo = registro.Cpf.SomenteNumeros();
            var nomeLimpo = registro.Nome.RemoverAcentos().Trim();
            var produtoLimpo = registro.Produto.RemoverAcentos().Trim();
            var numeroContratoLimpo = registro.Contrato.RemoverAcentos().Trim();

           
            //verificar se cliente existe
            var cliente = await _repository.ObterClientePorCpfAsync(cpfLimpo);

            if (cliente == null)
            {
                var clienteJaNaLista = importacao.Contratos.FirstOrDefault(c => c.Cliente.Cpf == cpfLimpo)?.Cliente;

                if (clienteJaNaLista != null)
                {
                    cliente = clienteJaNaLista;
                }
                else
                {
                    // Se o cliente não existe, cria um novo
                    cliente = new Cliente
                    {
                        Nome = nomeLimpo, 
                        Cpf = cpfLimpo
                    };
                }
            }

            // 4. Cria o Contrato com os dados limpos
            var contrato = new Contrato
            {
                NumeroContrato = numeroContratoLimpo,
                Produto = produtoLimpo,
                Vencimento = registro.Vencimento,
                Valor = registro.Valor,
                Cliente = cliente
            };

            importacao.Contratos.Add(contrato);
        }

        // 5. Grava apenas se houve linhas válidas
        if (importacao.Contratos.Any())
        {
            await _repository.SalvarImportacaoAsync(importacao);
        }
        else
        {
            throw new ArgumentException("Nenhum registo válido foi encontrado no ficheiro para importar.");
        }

        return listaDeErros;
    }
}