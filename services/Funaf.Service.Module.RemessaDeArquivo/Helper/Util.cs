using System;
using System.IO;
using System.Linq;
using System.Text;

namespace RemessaBB.FUNAF.Helper
{
    public static class Util
    {
        public static string PrepararCampo(string valorCampo, bool comVirgula = true)
        {
            if (valorCampo.Length > 0)
            {
                valorCampo = RemoverAcentos(valorCampo);
                if (comVirgula)
                    valorCampo = valorCampo.Replace(",", "");
                valorCampo = valorCampo.Replace(".", "");
                valorCampo = valorCampo.Replace("-", "");
                valorCampo = valorCampo.Replace("/", "");
                valorCampo = valorCampo.ToUpper();
            }
            else
            {
                Logger.LogWarning("RemessaBB.Helper.Util.PrepararCampo | O primeiro parâmetro está vazio!");
            }

            return valorCampo;
        }

        public static string RemoverAcentos(string texto)
        {
            if (texto.Length > 0)
            {
                var acentos = new[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
                var semAcento = new[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

                for (var i = 0; i < acentos.Length; i++)
                {
                    texto = texto.Replace(acentos[i], semAcento[i]);
                }

                texto = texto.Replace("(", "");
                texto = texto.Replace(")", "");

                string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

                texto = caracteresEspeciais.Aggregate(texto, (current, t) => current.Replace(t, ""));
                texto = texto.Replace("^\\s+", "");
                texto = texto.Replace("\\s+$", "");
                texto = texto.Replace("\\s+", " ");
            }
            else
            {
                Logger.LogWarning("RemessaBB.Helper.Util.RemoverAcentos | O primeiro parâmetro está vazio!");
            }

            return texto;
        }

        public static string FormataCampo(decimal valor, int numeroCaracteres, char valorSub, string tipoSub, bool isClean = false)
        {
            var item = string.Empty;

            var campo = valor.ToString("N2").Replace(".", "").Replace(",", "");

            if (isClean)
                campo = PrepararCampo(campo, false);

            if (campo.Length < numeroCaracteres)
            {
                switch (tipoSub)
                {
                    case "L":
                        item = campo.PadLeft(numeroCaracteres, valorSub);
                        break;
                    case "R":
                        item = campo.PadRight(numeroCaracteres, valorSub);
                        break;
                }
            }
            else if (campo.Length > numeroCaracteres)
            {
                item = campo.Substring(0, numeroCaracteres);
            }
            else
            {
                item = campo;
            }

            return item;
        }

        public static string FormataCampo(string campo, int numeroCaracteres, char valorSub, string tipoSub, bool isClean = false)
        {
            if (string.IsNullOrEmpty(campo))
                campo = "";

            var item = string.Empty;
            campo = campo.ToUpper();

            if (campo.Length > 0)
            {

                campo = RemoverAcentos(campo);

                if (isClean)
                    campo = PrepararCampo(campo, false);

                if (campo.Length < numeroCaracteres)
                {
                    switch (tipoSub)
                    {
                        case "L":
                            item = campo.PadLeft(numeroCaracteres, valorSub);
                            break;
                        case "R":
                            item = campo.PadRight(numeroCaracteres, valorSub);
                            break;
                    }
                }
                else if (campo.Length > numeroCaracteres)
                {
                    item = campo.Substring(0, numeroCaracteres);
                }
                else
                {
                    item = campo;
                }
            }
            else
            {
                Logger.LogWarning("RemessaBB.Helper.Util.FormataCampo | O primeiro parâmetro está vazio!");
            }

            return item;
        }

        public static void GerarArquivo(string dadosProntos, string fileName)
        {
            if (dadosProntos.Length > 0)
            {
                var sw = new StreamWriter(new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite), Encoding.Default);

                try
                {
                    sw.Write(dadosProntos);
                    sw.Flush();
                }
                catch (Exception ex)
                {
                    Logger.LogWarning("RemessaBB.Helper.Util.GerarArquivo | EXCEPTION");
                    Logger.LogException(ex, "RemessaBB.Helper.Util.GerarArquivo");
                }
                finally
                {
                    sw.Close();
                }
            }
            else
            {
                Logger.LogWarning("RemessaBB.Helper.Util.GerarArquivo | O primeiro parâmetro está vazio!");
            }
        }

    }
}