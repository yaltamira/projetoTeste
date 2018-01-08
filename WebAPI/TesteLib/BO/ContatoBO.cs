using System;
using System.Collections.Generic;
using TesteLib.BE;
using System.Linq;

namespace TesteLib
{
    public class ContatoBO
    {
        private DB.Contexto contexto;

        public ContatoBO()
        {
            contexto = new DB.Contexto();
        }

        public RespostaBE IncluirOuAlterarContato(ContatoBE contato)
        {
            var resposta = RespostaBE.NovaResposta();
            try
            {
                var validacao = ContatoValido(contato);
                if (validacao.Erro)
                {
                    return validacao;
                }
                resposta = GravarContato(contato);
            }
            catch (Exception ex)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add(ex.Message);
            }
            return resposta;
        }

        //public RespostaBE AlterarContato(ContatoBE contato)
        //{
        //    var resposta = RespostaBE.NovaResposta();
        //    try
        //    {
        //        var validacao = ContatoValido(contato);
        //        if (validacao.Erro)
        //        {
        //            return validacao;
        //        }

        //        GravarContato(contato);
        //    }
        //    catch (Exception ex)
        //    {
        //        resposta.Erro = true;
        //        resposta.Mensagem.Add(ex.Message);
        //    }
        //    return resposta;
        //}

        private RespostaBE ContatoValido(ContatoBE contato)
        {
            var retorno = RespostaBE.NovaResposta();

            if (contato == null)
            {
                retorno.Mensagem.Add("Nenhum contato foi informado");
                retorno.Erro = true;
                return retorno;
            }
            if (string.IsNullOrEmpty(contato.Nome))
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("Nome inválido");
            }
            if (string.IsNullOrEmpty(contato.Email))
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("Email inválido");
            }
            if (contato.DDD <= 0)
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("DDD inválido");
            }
            if (string.IsNullOrEmpty(contato.Telefone))
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("Número do telefone inválido");
            }
            if (contato.IdUsuario <= 0)
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("Usuário inválido");
            }

            return retorno;

        }

        private RespostaBE GravarContato(ContatoBE contato)
        {
            var retorno = RespostaBE.NovaResposta();
            try
            {
                using (var tran = contexto.Database.BeginTransaction())
                {
                    if (contato.IdContato == 0)
                        contexto.ContatoDB.Add(contato);
                    else
                        contexto.Entry(contato).State = System.Data.Entity.EntityState.Modified;
                    contexto.SaveChanges();
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                retorno.Erro = true;
                retorno.Mensagem.Add(ex.Message);
            }

            return retorno;
        }

        public RespostaBE ExcluirContato(int idContato)
        {
            var resposta = RespostaBE.NovaResposta();
            try
            {
                var contato = contexto.ContatoDB.Find(idContato);
                if (contato==null)
                {
                    resposta.Erro = true;
                    resposta.Mensagem.Add(string.Format("Nenhum contato foi localizado com o id {0}", idContato.ToString()));
                    return resposta;
                }

                contato.Ativo = false;
                GravarContato(contato);
            }
            catch (Exception ex)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add(ex.Message);
            }
            return resposta;
        }

        public ContatoBE SelecionarContato(int idContato)
        {
            if (idContato <= 0)
                return null;

            try
            {
                return contexto.ContatoDB.Find(idContato);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IEnumerable<ContatoBE> ListaContatos(int idUsuario)
        {
            if (idUsuario <= 0)
                return null;

            var contexto = new DB.Contexto();
            return contexto.ContatoDB
                .Where(c => c.IdUsuario == idUsuario && c.Ativo == true)
                .OrderBy(c => c.Nome)
                .ToList();
        }

        public static RespostaBE InativarContato(int idContato)
        {
            var resposta = RespostaBE.NovaResposta();

            if (idContato <= 0)
                return null;
            try
            {
                var contexto = new DB.Contexto();
                var contato = contexto.ContatoDB.Find(idContato);

                if (contexto == null)
                {
                    resposta.Erro = true;
                    resposta.Mensagem.Add("Nenhum contato foi localizado com este id");
                    return resposta;
                }

                using (var tran = contexto.Database.BeginTransaction())
                {
                    contato.Ativo = false;

                    contexto.Entry(contato).State = System.Data.Entity.EntityState.Modified;
                    contexto.SaveChanges();
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add(ex.Message);
            }

            return resposta;
        }
    }
}
