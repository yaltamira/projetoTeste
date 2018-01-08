using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TesteAPI.Tests.Lib
{
    [TestClass]
    public class Mail
    {
        [TestMethod]
        public void LembrarEmail()
        {
            var contexto = new TesteLib.DB.Contexto();
            string email = ConfigurationManager.AppSettings["EmailEndereco"];
            string senha = ConfigurationManager.AppSettings["EmailSenha"];
            var usuario = contexto.UsuarioDB.Where(u => u.Email.Trim().ToLower() == email.Trim().ToLower()).FirstOrDefault();

            TesteLib.BO.Mail mail = new TesteLib.BO.Mail(email, senha);
            mail.LembrarEmail(usuario);
        }
    }
}
