using SistemaChamadoTecnico.DAL;
using SistemaChamadoTecnico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadoTecnico.Controllers
{
    [Authorize]
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchCliente((int)id));
        }

        [HttpPost]
        public ActionResult Edit(int? id, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ChamadoTecnicoDb.AlterCliente(cliente);
                return RedirectToAction("List");
            }

            return View(ChamadoTecnicoDb.SearchCliente((int)id));
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchCliente((int)id));
        }

        [HttpPost]
        public ActionResult Delete(int? id, Cliente cliente)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            cliente.IdCliente = (int)id;
            ChamadoTecnicoDb.DeleteCliente(cliente);
            return RedirectToAction("List");
        }
    }
}