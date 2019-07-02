using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        public ActionResult Index()
        {
            ViewBag.Faturas = FaturaRepository.ListAll();
            ViewBag.Cartoes = CartaoRepository.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult Salvar()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Detalhes(long id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Remover(long id)
        {
            return RedirectToAction("Index");
        }
    }
}