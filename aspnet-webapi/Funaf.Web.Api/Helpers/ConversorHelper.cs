using System;

namespace Funaf.Web.Api.Helpers
{
    public abstract class ConversorHelper
    {
        public static DateTime FormatDataEn(DateTime data)
        // Inverter a data do padrão [Br] para o padrão [En]. Ou seja, Trocar [Dia] e [Mes].
        {
            var dt = data.ToString("MM/dd/yyyy");
            return DateTime.Parse(dt);
        }

        public static string base64Decode(string data)
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                var utf8Decode = encoder.GetDecoder();

                var todecode_byte = Convert.FromBase64String(data);
                var charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                var decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                var result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Decodificar um dado em Base64" + e.Message);
            }
        }

        public static string base64Encode(string data)
        {
            try
            {
                var encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                var encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Codificar um dado para Base64" + e.Message);
            }
        }
    }
}