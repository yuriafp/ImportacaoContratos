using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Application.Extensions
{
    public static class StringExtensions
    {
        //normaliza o texto para salvar na base de dados, removendo acentos e caracteres especiais
        public static string RemoverAcentos(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            var textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in textoNormalizado)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        //normaliza o texto para salvar na base de dados, removendo acentos, caracteres especiais e deixando apenas os números
        public static string SomenteNumeros(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            
           return new string(texto.Where(char.IsDigit).ToArray());
        }
    }
}
