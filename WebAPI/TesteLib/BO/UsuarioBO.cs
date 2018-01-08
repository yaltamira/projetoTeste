using System;
using System.Linq;
using TesteLib.BE;
using TesteLib.BO;

namespace TesteLib.BO
{
    public class UsuarioBO
    {
        private DB.Contexto contexto;

        public UsuarioBO()
        {
            contexto = new DB.Contexto();
        }

        public RetornoUsuario Logar(string email, string senha)
        {
            var retorno = new RetornoUsuario();
            retorno.Erro = false;
            retorno.Mensagem = new System.Collections.Generic.List<string>();

            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    retorno.Erro = true;
                    retorno.Mensagem.Add("E-mail não informado");
                    return retorno;
                }

                if (string.IsNullOrEmpty(senha))
                {
                    retorno.Erro = true;
                    retorno.Mensagem.Add("Senha não informada");
                    return retorno;
                }

                var contexto = new DB.Contexto();

                var usuario = contexto.UsuarioDB.Where(u =>
                    u.Email.Trim().ToUpper() == email.Trim().ToUpper()).FirstOrDefault();

                if(usuario == null)
                {
                    retorno.Erro = true;
                    retorno.Mensagem.Add("Usuário não localizado");
                    return retorno;
                }

                if(usuario.Senha != Criptografia.Encrypt(senha))
                {
                    retorno.Erro = true;
                    retorno.Mensagem.Add("Senha inválida");
                    return retorno;
                }

                usuario.Senha = "";
                retorno.Usuario = usuario;

                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Erro = true;
                retorno.Mensagem.Add(ex.Message);

                return retorno;
            }
        }

        public RespostaBE IncluirUsuario(UsuarioBE usuario)
        {
            var retorno = RespostaBE.NovaResposta();

            try
            {
                var validacao = UsuarioValido(usuario);
                if (validacao.Erro)
                {
                    return validacao;
                }
                if (ExisteEmail(usuario.Email))
                {
                    retorno.Erro = true;
                    retorno.Mensagem.Add("Este e-mail já está cadastrado");
                    return retorno;
                }
                usuario.Senha = Criptografia.Encrypt(usuario.Senha);

                retorno = GravarUsuario(usuario);

                return retorno;
            }
            catch (Exception ex)
            {
                retorno.Erro = true;
                retorno.Mensagem.Add(ex.Message);
                return retorno;
            }
        }

        private RespostaBE UsuarioValido(UsuarioBE usuario)
        {
            var retorno = RespostaBE.NovaResposta();

            if (usuario == null)
            {
                retorno.Mensagem.Add("Nenhum usuário foi informado");
                retorno.Erro = true;
                return retorno;
            }
            if (string.IsNullOrEmpty(usuario.Nome))
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("Nome inválido");
            }
            if (string.IsNullOrEmpty(usuario.Email)) {
                retorno.Erro = true;
                retorno.Mensagem.Add("Email inválido");
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                retorno.Erro = true;
                retorno.Mensagem.Add("Senha inválida");
            }

            return retorno;
        }

        public RespostaBE AlterarSenha(UsuarioBE usuario)
        {
            var resposta = RespostaBE.NovaResposta();

            try
            {
                var usuarioAlt = UsuarioBO.CarregarUsuarioPorId(usuario.IdUsuario);
                usuarioAlt.Senha = Criptografia.Encrypt(usuario.Senha);
                usuario = usuarioAlt;
                var validacao = UsuarioValido(usuario);

                if (validacao.Erro)
                {
                    return validacao;
                }

                resposta = AtualizarSenhaUsuario(usuario);
            }
            catch (Exception ex)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add(ex.Message);
            }

            return resposta;
        }

        private RespostaBE GravarUsuario(UsuarioBE usuario)
        {
            var retorno = RespostaBE.NovaResposta();
            try
            {
                using (var tran = contexto.Database.BeginTransaction())
                {
                    if (usuario.IdUsuario == 0)
                        contexto.UsuarioDB.Add(usuario);
                    else
                    {
                        var atUsuario = contexto.UsuarioDB.Find(usuario.IdUsuario);
                        atUsuario.Email = usuario.Email;
                        atUsuario.Nome = usuario.Nome;
                        contexto.Entry(atUsuario).State = System.Data.Entity.EntityState.Modified;
                    }
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

        private RespostaBE AtualizarSenhaUsuario(UsuarioBE usuario)
        {
            var retorno = RespostaBE.NovaResposta();
            try
            {
                using (var tran = contexto.Database.BeginTransaction())
                {
                    if (usuario.IdUsuario == 0)
                        throw new Exception("Usuário inválido!");
                    else
                    {
                        contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    }
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

        private bool ExisteEmail(string email)
        {
            bool resposta = 
                contexto.UsuarioDB.Any(u => u.Email.Trim().ToLower() == email.Trim().ToLower());

            return resposta;
        }

        public static UsuarioBE CarregarUsuarioPorId(int idUsuario)
        {
            try
            {
                var contexto = new DB.Contexto();
                return contexto.UsuarioDB.Find(idUsuario);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static RespostaBE LembrarSenha(string email, string emailSMTP, string senhaSMTP)
        {
            var resposta = RespostaBE.NovaResposta();

            var contexto = new DB.Contexto();
            var usuario = contexto.UsuarioDB.Where(u => u.Email.Trim().ToLower() == email.Trim().ToLower()).FirstOrDefault();

            if (usuario == null)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add("E-mail não localizado");
                return resposta;
            }

            var mail = new Mail(emailSMTP, senhaSMTP);
            resposta = mail.LembrarEmail(usuario);

            return resposta;
        }
    }

    public class RetornoUsuario : RespostaBE
    {
        public UsuarioBE Usuario { get; set; }
    }

}
