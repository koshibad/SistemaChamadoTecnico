using SistemaChamadoTecnico.DAL;
using SistemaChamadoTecnico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SistemaChamadoTecnico.Controllers
{
    [Authorize]
    public class ChamadoController : Controller
    {
        public static List<Estado> lstEstados = new List<Estado>()
        {
            new Estado(1,Estado.eEstado.Aguardando.ToString()),
            new Estado(2,Estado.eEstado.Pendente.ToString()),
            new Estado(3,Estado.eEstado.Finalizado.ToString())
        };

        [HttpGet]
        [Authorize(Roles = "Cliente")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public ActionResult Create(Chamado chamado)
        {
            if (!ModelState.IsValid)
                return View();

            chamado.DataCriacaoChamado = DateTime.Now;
            chamado.EstadoChamado = Estado.eEstado.Aguardando.ToString();
            chamado.IdCliente = ChamadoTecnicoDb.SearchPersonUser(0, User.Identity.GetUserId()).IdPerson;
            ChamadoTecnicoDb.CreateChamado(chamado);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Cliente")]
        public ActionResult List(string estado)
        {
            ViewBag.TodosEstados = new SelectList(lstEstados, "NomeEstado", "NomeEstado");
            var IdCliente = ChamadoTecnicoDb.SearchPersonUser(0, User.Identity.GetUserId()).IdPerson;
            return View(ChamadoTecnicoDb.ListChamado(estado).
                Where(x => x.IdCliente == IdCliente).ToList<Chamado>());
        }

        [HttpGet]
        [Authorize(Roles = "Cliente")]
        public ActionResult EditToClose(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
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
        [Authorize(Roles = "Cliente")]
        public ActionResult EditReopen(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
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
        [Authorize(Roles = "Cliente")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public ActionResult Delete(int? id, Chamado chamado)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            chamado.IdChamado = (int)id;
            ChamadoTecnicoDb.DeleteChamado(chamado);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = "Atendente")]
        public ActionResult ListFor()
        {
            return View(ChamadoTecnicoDb.ListChamado(Estado.eEstado.Aguardando.ToString()));
        }

        [HttpGet]
        [Authorize(Roles = "Atendente")]
        public ActionResult EditFor(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }

        [HttpPost]
        [Authorize(Roles = "Atendente")]
        public ActionResult EditFor(int? id, Chamado chamado)
        {
            if (ModelState.IsValid)
            {
                chamado.EstadoChamado = Estado.eEstado.Pendente.ToString();
                chamado.IdAtendente = ChamadoTecnicoDb.SearchPersonUser(0, User.Identity.GetUserId()).IdPerson;
                ChamadoTecnicoDb.AlterChamado(chamado);
                return RedirectToAction("ListFor");
            }

            return View(ChamadoTecnicoDb.SearchChamado((int)id));
        }
    }
}