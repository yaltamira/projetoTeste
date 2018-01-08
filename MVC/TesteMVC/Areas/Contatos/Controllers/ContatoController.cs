using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteMVC.Areas.Contatos.Models;

namespace TesteMVC.Areas.Contatos.Controllers
{
    public class ContatoController : Controller
    {
        private UsuarioModel usuario = new UsuarioModel();

        // GET: Contatos/Contato
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditarContato(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult EditarUsuario()
        {
            return View();
        }

    }
}