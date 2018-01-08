using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLib;
using TesteLib.BE;
using TesteLib.BO;

namespace TesteAPI.Tests.Lib
{
    [TestClass]
    public class Contato
    {
        [TestMethod]
        public void ValidacaoContatoNulo()
        {
            var contatoBO = new ContatoBO();
            var retorno = contatoBO.IncluirOuAlterarContato(null);

            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void ValidacaoContatoNenhumaInformacao()
        {
            var contatoBE = new ContatoBE();
            var contatoBO = new ContatoBO();

            var retorno = contatoBO.IncluirOuAlterarContato(contatoBE);


            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void IncluirContatoIdUsuarioInvalido()
        {
            var contatoBE = new ContatoBE();
            contatoBE.Nome = "Yuri Altamira";
            contatoBE.Email = "myura.batera@gmail.com";
            contatoBE.Ativo = true;
            contatoBE.DDD = 11;
            contatoBE.Telefone = "98765432";
            contatoBE.IdUsuario = 2;

            var contatoBO = new ContatoBO();
            var retorno = contatoBO.IncluirOuAlterarContato(contatoBE);
            Assert.IsTrue(retorno.Erro);
        }

        [TestMethod]
        public void IncluirContato()
        {
            var contatoBE = new ContatoBE();
            contatoBE.Nome = "Yuri Altamira 2";
            contatoBE.Email = "myura.batera@gmail.com";
            contatoBE.Ativo = true;
            contatoBE.DDD = 11;
            contatoBE.Telefone = "98765432";
            contatoBE.IdUsuario = 1;

            var contatoBO = new ContatoBO();
            var retorno = contatoBO.IncluirOuAlterarContato(contatoBE);
            Assert.IsFalse(retorno.Erro, retorno.Mensagem.ToString());
        }

        [TestMethod]
        public void ListaDeContatos()
        {
            var contatoBO = ContatoBO.ListaContatos(1);

            Assert.IsTrue(contatoBO.Count() == 2);
        }

        [TestMethod]
        public void AlterarContato()
        {
            var contatoBO = new ContatoBO();

            var contato = contatoBO.SelecionarContato(5);
            contato.Nome = "Teste teste";
            Assert.IsInstanceOfType(contato, typeof(ContatoBE));

            var resposta = contatoBO.IncluirOuAlterarContato(contato);

            Assert.IsFalse(resposta.Erro);
        }

        [TestMethod]
        public void InativarContato()
        {
            var resposta = ContatoBO.InativarContato(5);

            Assert.IsFalse(resposta.Erro);
        }

    }
}
