using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using myFinancas.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using myFinancas.MVC.Util;
using myFinancas.MVC.Models.Enuns;

namespace myFinancas.MVC.Controllers
{
    public class LancamentoController : Controller
    {
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());
        // GET: Lancamento
        public ActionResult Index(int pagina = 1)
        {
            try
            {
                pagina = pagina == 0 ? 1 : pagina;
                int qtdElementos = 10;
                IPagedList<LancamentoModel> lancametnos = this.lancamentoService.ListarTodosComFaturaECartao().ToPagedList(pagina, qtdElementos);

                ViewBag.active = "Lancamento";
                ViewBag.PagedList = lancametnos;
                ViewBag.PageNumber = CustomUtil<LancamentoModel>.caculaNumeroPagina(pagina, lancametnos);
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        public ActionResult Detalhes(long id)
        {
            LancamentoModel Lancamento = new LancamentoModel();

            if (id != 0) { Lancamento = this.lancamentoService.RecuperarPeloId(id); }
            ViewBag.Lancamento = Lancamento;
            ViewBag.active = "Lancamento";

            return PartialView();
        }
    }
}