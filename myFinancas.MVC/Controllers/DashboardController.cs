﻿using myFinancas.MVC.Repositories;
using myFinancas.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private DividaService dividaService = new DividaService(DividaRepository.getInstance());
        private CompradorService compradorService = new CompradorService(CompradorRepository.getInstance());

        public ActionResult Index()
        {
            ViewBag.active = "Dashboard";
            ViewBag.Dividas = this.dividaService.ListarDividasNaoPagas();
            ViewBag.Pagantes = this.compradorService.ListarTodosPagantes();
            ViewBag.Devedores = this.compradorService.ListarTodosDevedores();
            return View();
        }

        [HttpGet]
        public ActionResult Sidebar(string active)
        {
            ViewBag.active = active;
            return PartialView("_Sidebar");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}