using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using myFinancas.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Controllers
{
    public class LancamentoController : Controller
    {
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());
        // GET: Lancamento
        public ActionResult Detalhes(long id)
        {
            LancamentoModel Lancamento = new LancamentoModel();

            if (id != 0) { Lancamento = this.lancamentoService.RecuperarPeloId(id); }
            ViewBag.Lancamento = Lancamento;
            ViewBag.active = "Lancamentos";

            return PartialView();
        }
    }
}