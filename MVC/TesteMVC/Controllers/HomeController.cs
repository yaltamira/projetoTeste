using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CriarConta()
        {
            return View();
        }

        public ActionResult EsqueciMinhaSenha()
        {
            return View();
        }

        public ActionResult Logar()
        {
            var cookie = Request.Cookies["idUsuario"];
            if (cookie != null)
            {
                int _id;
                if (int.TryParse(cookie.Value, out _id))
                {
                    return RedirectToAction("contato", "contatos");
                }
            }

            return RedirectToAction("index", "home");
        }

        public ActionResult ResetSenha(string usuario, string hash)
        {
            ViewBag.usuario = usuario;
            ViewBag.hash = hash;
            return View();
        }

    }
}