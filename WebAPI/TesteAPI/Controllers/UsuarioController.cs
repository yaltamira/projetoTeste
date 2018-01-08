using System.Web.Http;
using TesteAPI.Models;
using TesteLib.BO;
using TesteLib.BE;
using System.Configuration;
using System.Collections.Generic;

namespace TesteAPI.Controllers
{
    public class UsuarioController : ApiController
    {

        // POST: api/Usuario/Logar
        [HttpPost]
        public RetornoUsuario Logar(LoginModel login)
        {
            var retorno = UsuarioRetornoModel.Logar(login);
            return retorno;

        }

        // POST: api/Usuario
        [HttpPost]
        public RetornoUsuario NovoCadastro(UsuarioBE usuario)
        {
            return UsuarioRetornoModel.Cadastrar(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public RetornoUsuario AlterarCadastro(UsuarioBE usuario)
        {
            return UsuarioRetornoModel.Alterar(usuario);
        }

        [HttpPost]
        public RespostaBE ReenviarSenha(string email)
        {
            string emailSMTP = ConfigurationManager.AppSettings["EmailEndereco"];
            string senhaSMTP = ConfigurationManager.AppSettings["EmailSenha"];

            var resposta = UsuarioBO.LembrarSenha(email, emailSMTP, senhaSMTP);

            return new RetornoUsuario() {
                Erro = resposta.Erro,
                Mensagem = resposta.Mensagem
            };
        }

        [HttpPost]
        public RespostaBE ConfirmarResetSenha(UsuarioBE usuario)
        {
            var resposta = new RespostaBE()
            {
                Erro = false,
                Mensagem = new List<string>()
            };

            var confirmacaoUsuario = UsuarioBO.CarregarUsuarioPorId(usuario.IdUsuario);
            if(confirmacaoUsuario.Senha != usuario.Senha)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add("Informações inválidas");
            }

            return resposta;
        }

        [HttpPost]
        public RespostaBE AlterarResetSenha(UsuarioBE usuario)
        {
            return UsuarioRetornoModel.AlterarSenha(usuario);
        }

    }
}
