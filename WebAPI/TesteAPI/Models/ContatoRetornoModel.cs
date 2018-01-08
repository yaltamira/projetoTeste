using System.Collections.Generic;
using TesteLib;
using TesteLib.BE;

namespace TesteAPI.Models
{
    public class ContatoRetornoModel
    {
        public static IEnumerable<ContatoBE> ListaContatos(int idUsuario)
        {
            return ContatoBO.ListaContatos(idUsuario);
        }

        public static ContatoBE SelecionaContato(int idContato)
        {
            var contato = new ContatoBO();
            return contato.SelecionarContato(idContato);
        }

        public static RespostaBE CadastrarOuAlterar(ContatoBE contato)
        {
            var contatoBO = new ContatoBO();
            var resposta = contatoBO.IncluirOuAlterarContato(contato);
            return resposta;
        }

        public static RespostaBE Inativar(int idContato)
        {
            var resposta = ContatoBO.InativarContato(idContato);
            return resposta;
        }

    }
}