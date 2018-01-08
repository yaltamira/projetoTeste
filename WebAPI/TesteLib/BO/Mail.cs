using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using TesteLib.BE;
using System.Web;

namespace TesteLib.BO
{
    public class Mail
    {
        private string EnderecoEmail { get; set; }
        private string SenhaEmail { get; set; }

        public Mail(string email, string senha)
        {
            EnderecoEmail = email;
            SenhaEmail = senha;
        }

        public RespostaBE LembrarEmail(UsuarioBE usuario)
        {
            var resposta = RespostaBE.NovaResposta();

            if (usuario == null)
            {
                resposta.Erro = true;
                resposta.Mensagem.Add("Usuário não informado");
                return resposta;
            }

            var corpoEmail = MontarEmail(usuario);
            if (string.IsNullOrEmpty(corpoEmail))
            {
                resposta.Erro = true;
                resposta.Mensagem.Add("Erro ao montar o corpo do e-mail");
                return resposta;
            }
            return EnviarEmail(usuario, corpoEmail);

        }

        private string MontarEmail(UsuarioBE usuario)
        {
            StringBuilder corpoEmail = new StringBuilder();

            corpoEmail.AppendLine(
                string.Format("http://yaltamira.somee.com/home/ResetSenha?usuario={0}&hash={1}",
                                usuario.IdUsuario,
                                HttpUtility.HtmlEncode(usuario.Senha).Replace("+", "%2B"))
            );
            return corpoEmail.ToString();
        }

        private RespostaBE EnviarEmail(UsuarioBE usuario, string corpoEmail)
        {
            var resposta = RespostaBE.NovaResposta();
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("myura.batera@gmail.com");
                mail.To.Add(usuario.Email);
                mail.Subject = "Envio de senha";
                mail.Body = corpoEmail;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(EnderecoEmail, SenhaEmail);

                smtp.Send(mail);

                resposta.Mensagem.Add("E-mail enviado com sucesso.");
                //smtp.SendCompleted += (s, e) =>
                //{
                //    smtp.Dispose();
                //};
                //smtp.SendAsync(mail, null);
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
