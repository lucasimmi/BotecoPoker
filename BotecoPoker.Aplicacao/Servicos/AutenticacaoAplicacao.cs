using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class AutenticacaoAplicacao
    {
        [Inject]
        public IUsuarioRepositorio UsuarioRepositorio { get; set; }

        public void AutenticarUsuario(string name, bool isPersistant, Usuario userData)
        {
            string data = null;
            if (userData != null)
                data = new JavaScriptSerializer().Serialize(userData);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(userData.Id, userData.Id.ToString(), DateTime.Now, DateTime.Now.AddYears(1), isPersistant, userData.Login, userData.Senha);
            string cookieData = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static Usuario ObterUsuarioLogado()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    var user = new Usuario
                    {
                        Id = Convert.ToInt32(ticket?.Name ?? "0"),
                        Nome = ticket.UserData,
                        Login = ticket.UserData,
                        Senha = ticket.CookiePath
                    };
                    return new DbContexto().Set<Usuario>().Find(user.Id);
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
