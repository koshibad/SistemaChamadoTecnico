using SistemaChamadoTecnico.DAL;
using SistemaChamadoTecnico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadoTecnico.Controllers
{
    public class ChamadoController : Controller
    {
        public static List<Estado> lstEstados = new List<Estado>()
        {
            new Estado(1,Estado.eEstado.Aguardando.ToString()),
            new Estado(2,Estado.eEstado.Pendente.ToString()),
            new Estado(3,Estado.eEstado.Finalizado.ToString()),
        };

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Chamado chamado)
        {
            if (!ModelState.IsValid)
                return View();

            chamado.DataCriacaoChamado = DateTime.Now;
            chamado.EstadoChamado = Estado.eEstado.Aguardando.ToString();
            ChamadoTecnicoDb.CreateChamado(chamado);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult List(string estado)
        {
            ViewBag.TodosEstados = new SelectList(lstEstados, "NomeEstado", "NomeEstado");
            return View(ChamadoTecnicoDb.ListChamado(estado));
        }

        [HttpGet]
        public ActionResult ListFor()
        {
            return View(ChamadoTecnicoDb.ListChamado(Estado.eEstado.Aguardando.ToString()));
        }

        [HttpGet]
        public ActionResult EditToClose(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        public ActionResult EditToClose(int? id, Chamado chamado)
        {
            if (ModelState.IsValid)
            {
                chamado = ChamadoTecnicoDb.SearchChamado((int)id);
                chamado.EstadoChamado = Estado.eEstado.Finalizado.ToString();
                chamado.DataEncerramentoChamado = DateTime.Now;
                ChamadoTecnicoDb.AlterChamado(chamado);
                return RedirectToAction("List");
            }

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpGet]
        public ActionResult EditReopen(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        public ActionResult EditReopen(int? id, Chamado chamado)
        {
            if (ModelState.IsValid)
            {
                chamado.EstadoChamado = Estado.eEstado.Aguardando.ToString();
                ChamadoTecnicoDb.AlterChamado(chamado);
                return RedirectToAction("List");
            }

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        public ActionResult Delete(int? id, Chamado chamado)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            chamado.IdChamado = (int)id;
            ChamadoTecnicoDb.DeleteChamado(chamado);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult EditFor(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        public ActionResult EditFor(int? id, Chamado chamado)
        {
            if (ModelState.IsValid)
            {
                chamado.EstadoChamado = Estado.eEstado.Pendente.ToString();
                ChamadoTecnicoDb.AlterChamado(chamado);
                return RedirectToAction("ListFor");
            }

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }
    }
}