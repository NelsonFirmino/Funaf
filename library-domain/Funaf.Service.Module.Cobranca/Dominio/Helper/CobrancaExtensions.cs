using Funaf.Service.Module.Cobranca.Dominio.Exceptions;
using System;
using System.Configuration;
using System.Globalization;
using System.Text;

namespace Funaf.Service.Module.Cobranca.Dominio.Helper
{
    public static class CobrancaExtensions
    {
        /// <summary>
        /// Converte um string para base64
        /// </summary>
        /// <param name="str">um texto</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string str)
        {
            var vtBytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(vtBytes);
        }

        /// <summary>
        /// Converte uma string para base64
        /// </summary>
        /// <param name="str">um texto</param>
        /// <returns></returns>
        public static string ToBase64Decode(this string str)
        {
            var vtBytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(vtBytes);
        }

        /// <summary>
        /// Recuperara os valores das configurações no AppConfig ou WebConfig
        /// </summary>
        /// <param name="key">Chave de Configuração</param>
        /// <returns>Valor armazenado</returns>
        public static string ReadConfig(this string key)
        {
            string config = string.Empty;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                config = appSettings[key] ?? "NÃO ENCONTRADO";
            }
            catch (ConfigurationErrorsException)
            {
                throw new CobrancaModuleException("Erro ao tentar ler o arquivo de configurações.");
            }

            return config;

        }

        /// <summary>
        /// Remover caracteres especiais e acentos
        /// </summary>
        /// <param name="str">um texto</param>
        /// <returns>texto sem caracteres especiais</returns>
        public static string RemoveAccents(this string str)
        {
            StringBuilder sbReturn = new StringBuilder();
            var vtText = str.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in vtText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
