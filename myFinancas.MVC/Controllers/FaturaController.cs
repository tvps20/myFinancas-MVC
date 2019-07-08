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
    public class FaturaController : Controller
    {
        private CartaoService cartaoService = new CartaoService(CartaoRepository.getInstance());
        private FaturaService faturaService = new FaturaService(FaturaRepository.getInstance());
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());
        private CompradorService compradorService = new CompradorService(CompradorRepository.getInstance());
        // GET: Fatura
        public ActionResult Index()
        {
            try
            {
                ViewBag.Faturas = faturaService.ListarTodos();
                ViewBag.Cartoes = cartaoService.ListarTodos();
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }           
        }

        [HttpPost]
        public ActionResult Salvar(FaturaModel Fatura)
        {
            try
            {
                this.faturaService.Salvar(Fatura);
                return RedirectToAction("Index").Mensagem("A fatura " + Fatura.DataVencimento.ToString("dd/MM/yyyy") + " foi salva com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpPost]
        public ActionResult SalvarLancamento(LancamentoModel Lancamento)
        {
            try
            {
                this.lancamentoService.Salvar(Lancamento);
                return RedirectToAction("Detalhes", "Fatura", new { id = Lancamento.IdFatura }).Mensagem("O lancamento de R$ " + Lancamento.Valor + " foi salvo com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Fatura", new { id = Lancamento.IdFatura }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult Detalhes(long id)
        {
            try
            {
                ViewBag.Fatura = this.faturaService.RecuperarPeloId(id);
                ViewBag.Lancamentos = this.lancamentoService.ListarTodosPelaFaturaIncludeComprador(id);
                ViewBag.Compradores = this.compradorService.ListarTodos();
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult Remover(long id)
        {
            try
            {
                this.faturaService.Remover(id);
                return RedirectToAction("Index").Mensagem("A fatura de id " + id + " foi removida com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult RemoverLancamento(long Id, long IdFatura)
        {
            try
            {
                this.lancamentoService.Remover(Id);
                return RedirectToAction("Detalhes", "Fatura", new { id = IdFatura }).Mensagem("O lancamento de id " + Id + " foi Removido com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Fatura", new { id = IdFatura }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }
    }
}