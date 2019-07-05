using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Controllers
{
    public class LancamentoController : Controller
    {
        // GET: Lancamento
        public ActionResult Detalhes(long id)
        {
            LancamentoModel Lancamento = new LancamentoModel();

            if (id != 0) { LancamentoRepository.RecuperarPeloId(id); }
            ViewBag.Lancamento = Lancamento;    
            
            return PartialView();
        }
    }
}