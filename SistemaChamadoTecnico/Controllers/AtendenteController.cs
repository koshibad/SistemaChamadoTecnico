using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

            string idUser = "";
            if (!CreateUser(atendente.SenhaUsuario, atendente.ConfirmaUsuario, atendente.NomeUsuario, ref idUser))
                return View();

            ChamadoTecnicoDb.CreateAtendente(atendente);
            ChamadoTecnicoDb.CreatePersonUser(idUser, atendente.IdAtendente);
            return RedirectToAction("Index", "Home");
        }

        private bool CreateUser(string Senha, string Confirma, string Nome, ref string idUser)
        {
            if (!ModelState.IsValid)
                return false;

            if (!Senha.Equals(Confirma))
            {
                ViewBag.Erro = "A senha e sua confimação não coincidem !";
                return false;
            }

            string funcao = Funcao.eFuncao.Atendente.ToString();
            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            //criando Role
            var role = roleManager.FindByName(funcao);
            if (role == null)
            {
                role = new IdentityRole(funcao);
                var roleresult = roleManager.Create(role);
                if (!roleresult.Succeeded)
                {
                    ViewBag.Erro = roleresult.Errors.FirstOrDefault();
                    return false;
                }
            }

            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

            //Cria uma identidade
            var usuarioInfo = new IdentityUser()
            {
                UserName = Nome
            };

            //Cria o usuário
            IdentityResult resultado = usuarioManager.Create(usuarioInfo, Senha);

            //se o usuário foi criado, o autentica
            if (resultado.Succeeded)
            {
                //Atribuindo Role ao User
                var rolesForUser = usuarioManager.GetRoles(usuarioInfo.Id);
                if (!rolesForUser.Contains(role.Name))
                    usuarioManager.AddToRole(usuarioInfo.Id, role.Name);

                idUser = usuarioInfo.Id;
                return true;
            }
            else
            {
                ViewBag.Erro = resultado.Errors.FirstOrDefault();
                return false;
            }
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

            string IdUser = "";
            atendente.IdAtendente = (int)id;
            ChamadoTecnicoDb.DeletePersonUser(atendente.IdAtendente, ref IdUser);
            DeleteUser(IdUser);
            ChamadoTecnicoDb.DeleteAtendente(atendente);
            return RedirectToAction("List");
        }

        private void DeleteUser(string idUser)
        {
            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

            var rolesForUser = usuarioManager.GetRoles(idUser);
            if (rolesForUser.Contains(Funcao.eFuncao.Atendente.ToString()))
                usuarioManager.RemoveFromRole(idUser, Funcao.eFuncao.Atendente.ToString());

            IdentityResult resultado = usuarioManager.Delete(usuarioManager.FindById(idUser));
        }
    }
}