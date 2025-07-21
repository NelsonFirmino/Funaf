namespace Funaf.Web.Api.ViewModels
{
    /// <summary>
    /// Instruções disponiveis no sistema.
    /// 
    /// DATA CRIAÇÃO: 29/08/2017
    /// AUTOR: Giovanni Nicoletti, [adicionar nome de quem alterou caso seja diferente do autor]
    /// Copiado do br.gov.rn.pge.util e replicado para o projeto do Sitad
    /// <br />
    /// <see cref="br.gov.rn.pge.dominio.BusinessObject"/>
    /// </summary>
    public class Instrucao
    {
        public const int Validar = 1;
        public const int Salvar = 2;
        public const int Remover = 3;
        public const int RecuperarPorId = 4;
        public const int BuscarNoAD = 5;
        public const int ListarTodos = 6;
        public const int ListarPorPerfil = 7;
        public const int ListarTodosAD = 8;
        public const int Pesquisar = 9;

        // Estes Status são apenas utilizado para geração de auditoria.
        public const int ListarAcoesServentia = 10;
        public const int ListarAcoesServentiaDetalhesPorAno = 11;
        public const int ListarAcoesServentiaDetalhesPorMes = 12;
        public const int ListarAcoesServentiaDetalhesPoDia = 13;
        public const int ListarDetalhesAcoes = 14;
        public const int ListarSistemasServentia = 15;
        public const int ListarAuditoriaSistema = 16;
        // Fim
        public const int ListarPorSistemas = 17;
        public const int ResetSenha = 18;
        public const int ListarPorServentia = 19;
        public const int AdicionarPerfil = 20;
        public const int RemoverPerfil = 21;

        public const int Enviar = 50;

        public const int ListarPorOrgao = 51;
        public const int ListarPorSetorAD = 52;
    }
}