using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
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
    [Authorize(Roles = "Admin")]
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

            string idUser = "";
            if (!CreateUser(cliente.SenhaUsuario, cliente.ConfirmaUsuario, cliente.NomeUsuario, ref idUser))
                return View();

            ChamadoTecnicoDb.CreateCliente(cliente);
            ChamadoTecnicoDb.CreatePersonUser(idUser, cliente.IdCliente);
            return RedirectToAction("Index", "Home");
        }

        private bool CreateUser(string Senha, string Confirma, string Nome, ref string idUser)
        {
            if (!ModelState.IsValid)
                return false;

            if (Nome == null)
            {
                ViewBag.Erro = "O usuário é obirgatório !";
                return false;
            }

            if (Senha == null || Confirma == null || !Senha.Equals(Confirma))
            {
                ViewBag.Erro = "A senha e sua confimação não coincidem !";
                return false;
            }

            string funcao = Funcao.eFuncao.Cliente.ToString();
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

            string IdUser = "";
            cliente.IdCliente = (int)id;
            ChamadoTecnicoDb.DeletePersonUser(cliente.IdCliente, ref IdUser);
            DeleteUser(IdUser);
            ChamadoTecnicoDb.DeleteCliente(cliente);
            return RedirectToAction("List");
        }

        private void DeleteUser(string idUser)
        {
            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

            var rolesForUser = usuarioManager.GetRoles(idUser);
            if (rolesForUser.Contains(Funcao.eFuncao.Cliente.ToString()))
                usuarioManager.RemoveFromRole(idUser, Funcao.eFuncao.Cliente.ToString());

            IdentityResult resultado = usuarioManager.Delete(usuarioManager.FindById(idUser));
        }
    }
}