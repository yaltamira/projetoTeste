using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TesteAPI.Models;
using TesteLib;
using TesteLib.BE;

namespace TesteAPI.Controllers
{
    public class ContatoController : ApiController
    {
        
        [HttpGet]
        public IEnumerable<ContatoBE> ListaContatos(string id)
        {
            int _id;
            if (int.TryParse(id, out _id))
            {
                return ContatoRetornoModel.ListaContatos(_id);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ContatoBE SelecionaContato(string id)
        {
            int _id;
            if (int.TryParse(id, out _id))
            {
                return ContatoRetornoModel.SelecionaContato(_id);
            }
            else
            {
                return null;
            }
        }

        //Cadastrar
        [HttpPost]
        public RespostaBE CadastrarOuAlterar(ContatoBE contato)
        {
            return ContatoRetornoModel.CadastrarOuAlterar(contato);
        }

        //Cadastrar
        [HttpDelete]
        public RespostaBE Deletar(string id)
        {
            int _id;
            if (int.TryParse(id, out _id))
            {
                return ContatoRetornoModel.Inativar(_id);
            }
            else
            {
                return null;
            }
        }


    }
}
