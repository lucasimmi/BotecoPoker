using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class FiltroTorneioCliente : FiltroBase
    {
        public string NomeCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string ApelidoCliente { get; set; }

        public FiltroTorneioCliente(string param1, string param2, string param3)
        {
            NomeCliente = param1;
            CodigoCliente = param2;
            ApelidoCliente = param3;
        }
        public FiltroTorneioCliente()
        {

        }
    }
}
