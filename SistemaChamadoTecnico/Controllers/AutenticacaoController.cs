using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
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