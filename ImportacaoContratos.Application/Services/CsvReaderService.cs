using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using ImportacaoContratos.Application.DTOs;

namespace ImportacaoContratos.Application.Services;

public class CsvReaderService
{
    public IEnumerable<ContratoCsvRecord> LerContratosDeCsv(Stream fileStream)
    {
        //encoding para ler arquivos com acentos e caracteres especiais ISO
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding("iso-8859-1");

        using var reader = new StreamReader(fileStream, encoding);

        var culture = new CultureInfo("pt-BR");

        var config = new CsvConfiguration(culture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            MissingFieldFound = null
        };

        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<ContratoCsvRecord>().ToList();
    }
}