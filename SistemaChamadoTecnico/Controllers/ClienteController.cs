using SistemaChamadoTecnico.DAL;
using SistemaChamadoTecnico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadoTecnico.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();

            ChamadoTecnicoDb.CreateCliente(cliente);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(ChamadoTecnicoDb.ListCliente());
        }
    }
}