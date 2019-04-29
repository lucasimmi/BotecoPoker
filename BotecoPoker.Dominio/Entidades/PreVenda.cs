using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.Entidades
{
    public class PreVenda : Entidade<long>
    {
        public short Quantidade { get; set; }
        public virtual Produto Produto { get; set; }
        public int IdProduto { get; set; }
        public DateTime DataHora { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public long? IdVenda { get; set; }
        public virtual Venda Venda { get; set; }
        public Ativo Ativo { get; set; }

        public PreVenda()
        {
            NomeProduto = Produto?.Nome ?? "";
        }

        /// <summary>
        /// Propriedades Web ---\/---
        /// </summary>
        public string NomeProduto { get; set; }
    }
}
