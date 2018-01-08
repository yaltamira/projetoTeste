using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace TesteMVC.Areas.Contatos.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public UsuarioModel()
        {

            var idUsuario = getValueCookie(HttpContext.Current.Request.Cookies.Get("idUsuario"));

            if (!string.IsNullOrEmpty(idUsuario))
            {
                int _id;
                if (int.TryParse(idUsuario, out _id))
                {
                    this.IdUsuario = _id;
                    this.Nome = getValueCookie(HttpContext.Current.Request.Cookies.Get("nomeUsuario"));
                    this.Email = getValueCookie(HttpContext.Current.Request.Cookies.Get("emailUsuario"));

                }
            }
        }

        private string getValueCookie(HttpCookie httpCookie)
        {
            StringWriter st = new StringWriter();
            string retorno = "";

            if (httpCookie == null)
            {
                return "";
            }
            else
            {
                if (!string.IsNullOrEmpty(httpCookie.Value))
                {
                    HttpUtility.HtmlDecode(httpCookie.Value, st);
                    retorno = HttpContext.Current.Server.UrlDecode(st.ToString());
                }
            }
            return retorno;
        }
    }
}