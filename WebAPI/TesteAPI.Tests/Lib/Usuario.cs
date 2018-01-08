using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLib;
using TesteLib.BO;
using TesteLib.BE;

namespace TesteAPI.Tests.Lib
{
    [TestClass]
    public class Usuario
    {
        [TestMethod]
        public void ValidacaoUsuarioNulo()
        {
            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(null);


            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void ValidacaoUsuarioNenhumaInformacao()
        {
            var usuarioBE = new UsuarioBE();
            usuarioBE.Nome = "";
            usuarioBE.Email = "";
            usuarioBE.Senha = "";

            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(usuarioBE);


            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void ValidacaoUsuario_ComNome_SemEmail_SemSenha()
        {
            var usuarioBE = new UsuarioBE();
            usuarioBE.Nome = "Yuri Altamira";

            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(usuarioBE);


            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void ValidacaoUsuario_ComNome_ComEmail_SemSenha()
        {
            var usuarioBE = new UsuarioBE();
            usuarioBE.Nome = "Yuri Altamira";
            usuarioBE.Email = "myura.batera@gmail.com";

            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(usuarioBE);


            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void ValidacaoUsuario_SemNome_ComEmail_ComSenha()
        {
            var usuarioBE = new UsuarioBE();
            usuarioBE.Email = "myura.batera@gmail.com";
            usuarioBE.Senha = "123456";

            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(usuarioBE);


            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void IncluirUsuario()
        {
            var usuarioBE = new UsuarioBE();
            usuarioBE.Nome = "Yuri Altamira";
            usuarioBE.Email = "myura.batera@gmail.com";
            usuarioBE.Senha = "123456";

            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(usuarioBE);
            Assert.IsFalse(retorno.Erro, retorno.Mensagem.ToString());
        }

        [TestMethod]
        public void IncluirUsuarioJaCadastrado()
        {
            var usuarioBE = new UsuarioBE();
            usuarioBE.Nome = "Yuri Altamira";
            usuarioBE.Email = "myura.batera@gmail.com";
            usuarioBE.Senha = "123456";

            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.IncluirUsuario(usuarioBE);
            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void LogarSemDados()
        {
            var usuario = new UsuarioBO();
            var resposta = usuario.Logar(null, null);

            Assert.IsNull(resposta);
        }

        [TestMethod]
        public void LogarComDadosInvalidos()
        {
            var usuario = new UsuarioBO();
            var resposta = usuario.Logar("yuri", "123456");

            Assert.IsNull(resposta);
        }

        [TestMethod]
        public void Logar()
        {
            var usuario = new UsuarioBO();
            var resposta = usuario.Logar("myura.batera@gmail.com", "123456");

            Assert.IsInstanceOfType(resposta, typeof(UsuarioBE));
        }

        [TestMethod]
        public void AlterarUsuario()
        {
            bool valido = false;
            var usuarioBO = new UsuarioBO();
            var retorno = usuarioBO.Logar("myura.batera@gmail.com", "123456");
            if (!retorno.Erro)
            {
                retorno.Usuario.Nome = "Yuri Altamira";
                
                valido = usuarioBO.IncluirUsuario(retorno.Usuario).Erro;
            }

            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void CarregarUsuarioPorId()
        {
            var usuario = UsuarioBO.CarregarUsuarioPorId(7);

            Assert.IsInstanceOfType(usuario, typeof(UsuarioBE));
        }

        [TestMethod]
        public void CarregarUsuarioPorIdInvalido()
        {
            var usuario = UsuarioBO.CarregarUsuarioPorId(1);
            Assert.IsNull(usuario);

        }

    }
}
