using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class UsuarioAplicacao
    {
        [Inject]
        public IUsuarioRepositorio UsuarioRepositorio { get; set; }

        [Inject]
        public IDbContexto Contexto { get; set; }

        [Inject]
        public AutenticacaoAplicacao AutenticacaoAplicacao { get; set; }

        public List<Usuario> ListarUsuarios()
        {
            return UsuarioRepositorio.Query().ToList();
        }

        public string ValidarUsuario(Usuario usuario)
        {
            StringBuilder erros = new StringBuilder();
            if (!usuario.Nome.TemValor())
                erros.AppendLine("Favor preencher o nome. ");
            if (!usuario.Login.TemValor())
                erros.AppendLine("Favor preencher o login. ");

            if (!usuario.Senha.TemValor() || !usuario.ConfirmaSenha.TemValor())
                erros.AppendLine("Favor digitar senha e confirmação da senha. ");
            else if (usuario.Senha != usuario.ConfirmaSenha)
                erros.AppendLine("Senhas não confere, favor digite novamente.");

            return erros.ToString();
        }

        public string ValidarAlteracao(Usuario usuario)
        {
            StringBuilder erros = new StringBuilder();
            erros.Append(ValidarUsuario(usuario));
            var usuarioAtual = AutenticacaoAplicacao.ObterUsuarioLogado();
            if (!(usuario.NovaSenha.TemValor() && usuario.ConfimaNovaSenha.TemValor() && usuario.NovaSenha == usuario.ConfimaNovaSenha && usuario.Senha == usuarioAtual.Senha))
            {
                erros.AppendLine("Senhas não conferem para alteração da senha!");
            }
            return erros.ToString();
        }

        public void AlterarUsuario(Usuario usuario)
        {
            var user = UsuarioRepositorio.Buscar(usuario.Id);
            user.Login = usuario.Login;
            user.Nome = usuario.Nome;
            user.Senha = usuario.NovaSenha;
            UsuarioRepositorio.Atualizar(user);
            Contexto.Salvar();
        }

        public void GravarUsuario(Usuario usuario)
        {
            UsuarioRepositorio.Cadastrar(usuario);
            Contexto.Salvar();
        }

        public Usuario ObterDadosUsuarioLogado()
        {
            var usuarioLogado = AutenticacaoAplicacao.ObterUsuarioLogado();
            return UsuarioRepositorio.Buscar(usuarioLogado.Id);
        }
    }
}
