using BotecoPoker.Dominio.Utils;
using System.Web.DynamicData;

namespace BotecoPoker.Dominio.Entidades
{
    public class Usuario : Entidade<int>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string NovaSenha { get; set; }
        public string ConfimaNovaSenha { get; set; }
    }
}
