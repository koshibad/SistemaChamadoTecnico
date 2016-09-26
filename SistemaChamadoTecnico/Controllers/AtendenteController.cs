using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadoTecnico.Controllers
{
    public class AtendenteController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}