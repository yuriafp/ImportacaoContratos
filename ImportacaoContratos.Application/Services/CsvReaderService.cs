using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ImportacaoContratos.Application.DTOs;

namespace ImportacaoContratos.Application.Services;

public class CsvReaderService
{
    public IEnumerable<ContratoCsvRecord> LerContratosDeCsv(Stream fileStream)
    {
        throw new NotImplementedException();
    }
}