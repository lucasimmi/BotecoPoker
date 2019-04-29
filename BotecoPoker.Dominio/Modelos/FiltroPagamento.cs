using BotecoPoker.Dominio.Utils;
using System;

namespace BotecoPoker.Dominio.modelos
{
    public class FiltroPagamento : FiltroBase
    {
        public string NomeCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string ApelidoCliente { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool? Tudo { get; set; }

        public FiltroPagamento()
        {

        }

        public FiltroPagamento(string param1, string param2, string param3)
        {
            NomeCliente = param1;
            ApelidoCliente = param2;
            CodigoCliente = param3;
        }

        public FiltroPagamento(string param1, string param2, DateTime? param3, bool? param4)
        {
            NomeCliente = param1;
            ApelidoCliente = param2;
            DataPagamento = param3;
            Tudo = param4;
        }
    }
}
