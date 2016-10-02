using SistemaChamadoTecnico.DAL;
using SistemaChamadoTecnico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadoTecnico.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchAtendente((int)id));
        }

        [HttpPost]
        public ActionResult Edit(int? id, Atendente atendente)
        {
            if (ModelState.IsValid)
            {
                ChamadoTecnicoDb.AlterAtendente(atendente);
                return RedirectToAction("List");
            }

            return View(ChamadoTecnicoDb.SearchAtendente((int)id));
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchAtendente((int)id));
        }

        [HttpPost]
        public ActionResult Delete(int? id, Atendente atendente)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            atendente.IdAtendente = (int)id;
            ChamadoTecnicoDb.DeleteAtendente(atendente);
            return RedirectToAction("List");
        }
    }
}