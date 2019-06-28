using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Controllers
{
    public class CartaoController : Controller
    {
        // GET: Cartao
        public ActionResult Index()
        {
            ViewBag.Cartoes = CartaoRepository.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult Salvar(CartaoModel Cartao)
        {
            try
            {
                CartaoRepository.Salvar(Cartao);             
            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index");
        }
    }
}