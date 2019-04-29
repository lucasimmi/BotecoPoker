using BotecoPoker.Aplicacao.Servicos;
using Ninject;
using System.Web.Mvc;
using System.Web.Security;

namespace BotecoPoker.Mvc.Controllers
{
    public class LoginController : Controller
    {
        [Inject]
        public LoginAplicacao LoginAplicacao { get; set; }

        [Inject]
        public AutenticacaoAplicacao Autenticacao { get; set; }

        public ActionResult Index()
        {
            return View("Login");
        }
       
        [HttpPost]
        public ActionResult Login(string usuario, string senha)
        {
            var user = LoginAplicacao.Login(usuario, senha);
            if (user != null)
            {
                Autenticacao.AutenticarUsuario(user.Id.ToString(), false, user);
                return RedirectToAction("TelaInicial", "Home");
            }
            ViewBag.erro = "Usuario ou senha incorretos";
            return View("Login");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
           return RedirectToAction("Index", "Login");
        }
    }
}