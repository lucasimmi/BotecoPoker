using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.Entidades
{
    public class Caixa : Entidade<long>
    {
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public int IdUsuarioAbertura { get; set; }
        public int? IdUsuarioFechamento { get; set; }
        public Dictionary<string,double> ValorTorneios{ get; set; }
        public double? ValorVenda { get; set; }
        public double? ValorCashGame { get; set; }
        public Ativo Ativo { get; set; }
    }
}
