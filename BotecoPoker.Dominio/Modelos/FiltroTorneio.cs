using BotecoPoker.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class FiltroTorneio : FiltroBase
    {
        public string Nome { get; set; }
        public Ativo? Ativo { get; set; }

        public FiltroTorneio(string param1)
        {
            Nome = param1;
        }
        public FiltroTorneio()
        {

        }
    }
}
