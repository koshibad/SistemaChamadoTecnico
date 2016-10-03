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
    public class AutenticacaoController : Controller
    {
        public static List<Funcao> lstFuncoes = new List<Funcao>()
        {
            new Funcao(1,Funcao.eFuncao.Admin.ToString()),
            //new Funcao(2,Funcao.eFuncao.Atendente.ToString()),
            //new Funcao(3,Funcao.eFuncao.Cliente.ToString())
        };

        [HttpGet]
        public ActionResult Edit()
        {
            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);
            var identityUser = usuarioManager.FindById(User.Identity.GetUserId());

            return View(new Usuario() { Nome = identityUser.UserName, Id = identityUser.Id });
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (!usuario.Senha.Equals(usuario.Confirma))
            {
                ViewBag.Erro = "A senha e sua confimação não coincidem !";
                return View(usuario);
            }

            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);
            var identityUser = usuarioManager.FindById(User.Identity.GetUserId());
            bool trocouNome = !identityUser.Equals(usuario.Nome);
            if (trocouNome)
            {
                identityUser.UserName = usuario.Nome;
                usuarioManager.Update(identityUser);
            }

            usuarioManager.ChangePassword(identityUser.Id, usuario.SenhaAntiga, usuario.Senha);

            if (trocouNome)
            {
                var autManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var identidadeUsuario = usuarioManager.CreateIdentity(identityUser,
                    DefaultAuthenticationTypes.ApplicationCookie);
                autManager.SignIn(new AuthenticationProperties() { }, identidadeUsuario);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario, string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);
            var usuarioInfo = usuarioManager.Find(usuario.Nome, usuario.Senha);

            if (usuarioInfo == null)
            {
                ViewBag.Erro = "Usuário ou senha inválidos";
                return View();
            }

            var autManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            var identidadeUsuario = usuarioManager.CreateIdentity(usuarioInfo,
                DefaultAuthenticationTypes.ApplicationCookie);

            autManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = false
            },
            identidadeUsuario);

            if (string.IsNullOrEmpty(ReturnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(ReturnUrl);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var autManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            autManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TodasFuncoes = new SelectList(lstFuncoes, "NomeFuncao", "NomeFuncao");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            if (!usuario.Senha.Equals(usuario.Confirma))
            {
                ViewBag.Erro = "A senha e sua confimação não coincidem !";
                return View();
            }

            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            //criando Role
            var role = roleManager.FindByName(usuario.Funcao);
            if (role == null)
            {
                role = new IdentityRole(usuario.Funcao);
                var roleresult = roleManager.Create(role);
                if (!roleresult.Succeeded)
                {
                    ViewBag.Erro = roleresult.Errors.FirstOrDefault();
                    return View();
                }
            }

            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

            //Cria uma identidade
            var usuarioInfo = new IdentityUser()
            {
                UserName = usuario.Nome
            };

            //Cria o usuário
            IdentityResult resultado = usuarioManager.Create(usuarioInfo, usuario.Senha);

            //se o usuário foi criado, o autentica
            if (resultado.Succeeded)
            {
                //Atribuindo Role ao User
                var rolesForUser = usuarioManager.GetRoles(usuarioInfo.Id);
                if (!rolesForUser.Contains(role.Name))
                    usuarioManager.AddToRole(usuarioInfo.Id, role.Name);

                //Autentica e volta para a página inicial
                var autManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var identidadeUsuario = usuarioManager.CreateIdentity(usuarioInfo,
                    DefaultAuthenticationTypes.ApplicationCookie);
                autManager.SignIn(new AuthenticationProperties() { }, identidadeUsuario);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Erro = resultado.Errors.FirstOrDefault();
                return View();
            }
        }
    }
}