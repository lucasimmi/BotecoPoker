using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class FiltroCliente : FiltroBase
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Codigo { get; set; }

        public FiltroCliente(string param1, string param2, string param3)
        {
            Nome = param1;
            Apelido = param2;
            Codigo = param3;
        }

        public FiltroCliente()
        {
        }
    }
}
