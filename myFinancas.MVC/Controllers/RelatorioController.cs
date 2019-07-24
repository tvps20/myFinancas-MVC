using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Models.Enuns;
using myFinancas.MVC.Repositories;
using myFinancas.MVC.Services;
using myFinancas.MVC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Controllers
{
    public class RelatorioController : Controller
    {
        private CompradorService compradorService = new CompradorService(CompradorRepository.getInstance());
        private DividaService dividaService = new DividaService(DividaRepository.getInstance());
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());
        private RelatorioService relatorioService = new RelatorioService();
        // GET: Relatorio
        public ActionResult Index()
        {
            try
            {
                ViewBag.active = "Relatorio";
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public void CompradorRelatorio(long idComprador)
        {
            List<DividaModel> dividas = this.dividaService.ListarTodasDividasCompradorNPagas(idComprador);
            Dictionary<string, List<LancamentoModel>> Lancamentos = this.lancamentoService.ListarLancamentosPorDividas(dividas);
            Dictionary<string, List<LancamentoModel>> lancamentosAtuais = this.lancamentoService.ListarLancamentosDoCompradorAtuais(idComprador);
            CompradorModel comprador = this.compradorService.RecuperarPeloId(idComprador);

            this.relatorioService.ExportToCsv(dividas, Lancamentos, lancamentosAtuais, comprador, Response);
        }
    }
}