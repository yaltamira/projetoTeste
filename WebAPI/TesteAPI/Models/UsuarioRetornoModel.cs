using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteLib.BE;
using TesteLib.BO;

namespace TesteAPI.Models
{
    public class UsuarioRetornoModel
    {
        public UsuarioBE Usuario { get; set; }

        public static RetornoUsuario Logar(LoginModel login)
        {
            try
            {
                var usuarioBO = new UsuarioBO();
                var usuarioRetorno = usuarioBO.Logar(login.Login, login.Senha);

                return usuarioRetorno;
            }
            catch (Exception ex)
            {
                var ret = new RetornoUsuario();
                ret.Erro = true;
                ret.Mensagem = new List<string>();
                ret.Mensagem.Add(ex.Message);

                return ret;
            }
        }

        public static RetornoUsuario Cadastrar(UsuarioBE usuario)
        {
            var retorno = new RetornoUsuario();
            try
            {
                var usuarioBO = new UsuarioBO();
                var usuarioRetorno = usuarioBO.IncluirUsuario(new UsuarioBE()
                {
                    IdUsuario = usuario.IdUsuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                });

                retorno.Erro = usuarioRetorno.Erro;
                retorno.Mensagem = usuarioRetorno.Mensagem;
                retorno.Usuario = usuario;

                return retorno;
            }
            catch (Exception ex)
            {
                var ret = new RetornoUsuario();
                ret.Erro = true;
                ret.Mensagem = new List<string>();
                ret.Mensagem.Add(ex.Message);

                return ret;
            }

        }

        public static RetornoUsuario Alterar(UsuarioBE usuario)
        {
            var retorno = new RetornoUsuario();
            try
            {
                var usuarioBO = new UsuarioBO();
                var usuarioAlterado = usuarioBO.AlterarSenha(usuario);

                retorno.Erro = usuarioAlterado.Erro;
                retorno.Mensagem = usuarioAlterado.Mensagem;
                retorno.Usuario = usuario;

                return retorno;
            }
            catch (Exception ex)
            {
                var ret = new RetornoUsuario();
                ret.Erro = true;
                ret.Mensagem = new List<string>();
                ret.Mensagem.Add(ex.Message);

                return ret;
            }

        }

        public static RetornoUsuario AlterarSenha(UsuarioBE usuario)
        {
            var retorno = new RetornoUsuario();
            try
            {
                var usuarioBO = new UsuarioBO();
                var usuarioAlterado = usuarioBO.AlterarSenha(usuario);

                retorno.Erro = usuarioAlterado.Erro;
                retorno.Mensagem = usuarioAlterado.Mensagem;
                retorno.Usuario = usuario;

                return retorno;
            }
            catch (Exception ex)
            {
                var ret = new RetornoUsuario();
                ret.Erro = true;
                ret.Mensagem = new List<string>();
                ret.Mensagem.Add(ex.Message);

                return ret;
            }

        }
    }
}