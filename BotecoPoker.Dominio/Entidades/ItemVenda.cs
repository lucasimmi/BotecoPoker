using BotecoPoker.Dominio.Utils;

namespace BotecoPoker.Dominio.Entidades
{
    public class ItemVenda : Entidade<long>
    {
        public virtual Produto Produto { get; set; }
        public int IdProduto { get; set; }
        public double ValorProduto { get; set; }
        public short QtdProduto { get; set; }
        public virtual Venda Venda { get; set; }
        public long IdVenda { get; set; }
        public double ValorTotal { get; set; }
    }
}
