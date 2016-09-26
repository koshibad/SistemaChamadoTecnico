using SistemaChamadoTecnico.DAL;
using SistemaChamadoTecnico.Models;
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Atendente atendente)
        {
            if (!ModelState.IsValid)
                return View();

            ChamadoTecnicoDb.CreateAtendente(atendente);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(ChamadoTecnicoDb.ListAtendente());
        }
    }
}