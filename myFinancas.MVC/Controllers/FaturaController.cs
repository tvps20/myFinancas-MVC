using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Models.Enuns;
using myFinancas.MVC.Repositories;
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
        // GET: Fatura
        public ActionResult Index()
        {
            try
            {
                List<FaturaModel> Faturas = FaturaRepository.ListAll();
                List<CartaoModel> Cartoes = CartaoRepository.ListAll();
                ViewBag.Faturas = Faturas != null ? Faturas : new List<FaturaModel>();
                ViewBag.Cartoes = Cartoes != null ? Cartoes : new List<CartaoModel>();
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
                FaturaRepository.Salvar(Fatura);
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
                LancamentoRepository.Salvar(Lancamento);
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
                FaturaModel Fatura = FaturaRepository.RecuperarPeloId(id);
                List<LancamentoModel> Lancamentos = LancamentoRepository.ListAllByFatura(id);
                List<CompradorModel> Compradores = CompradorRepository.ListAll();
                ViewBag.Fatura = Fatura != null ? Fatura : new FaturaModel();
                ViewBag.Lancamentos = Lancamentos != null ? Lancamentos : new List<LancamentoModel>();
                ViewBag.Compradores = Compradores != null ? Compradores : new List<CompradorModel>();
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
                FaturaRepository.Remover(id);
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
                LancamentoRepository.Remover(Id);
                return RedirectToAction("Detalhes", "Fatura", new { id = IdFatura }).Mensagem("O lancamento de id " + Id + " foi Removido com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Fatura", new { id = IdFatura }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }
    }
}