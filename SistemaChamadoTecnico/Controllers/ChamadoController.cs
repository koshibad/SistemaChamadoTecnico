﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadoTecnico.Controllers
{
    public class ChamadoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}