using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funaf.Domain.Module.Lancamentos.Dominio.Tabelas
{
    public class Tabela
    {
        public int Id { get; set; }
        public string Discriminacao { get; set; }
        public decimal Valor { get; set; }
        public GrpServico GrupoServicos { get; set; }
        public int Quantidade { get; set; }
    }
}