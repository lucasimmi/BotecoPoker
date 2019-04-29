using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.Enumeradores
{
    public enum TipoFinalizador : short
    {
        Aberto = 0,
        Dinheiro = 1,
        [Description("Cartão de Débito")]
        Debito = 2,
        [Description("Cartão de Crédito")]
        Credito = 3,
        Cheque = 4,
        Cortezia = 5
    }
}
