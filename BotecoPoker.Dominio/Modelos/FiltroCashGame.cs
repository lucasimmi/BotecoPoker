using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class FiltroCashGame : FiltroBase
    {
        public string NomeCliente { get; set; }
        public double Valor { get; set; }
        public SituacaoVenda Situacao { get; set; }

        public FiltroCashGame(string param1, double? param2, short? param3)
        {
            NomeCliente = param1;
            Valor = param2 ?? 0;
            Situacao = (SituacaoVenda)(param3 ?? 2);
        }

        public FiltroCashGame()
        {

        }
    }
}
