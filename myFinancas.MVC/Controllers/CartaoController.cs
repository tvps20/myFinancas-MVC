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
    public class CartaoController : Controller
    {
        // GET: Cartao
        public ActionResult Index()
        {
            try
            {
                List<CartaoModel> Cartoes = CartaoRepository.ListAll();
                ViewBag.Cartoes = Cartoes != null ? Cartoes : new List<CartaoModel>();
                return View("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.DANGER), "error");
            }
        }

        [HttpPost]
        public ActionResult Salvar(CartaoModel Cartao)
        {
            try
            {
                CartaoRepository.Salvar(Cartao);
                return RedirectToAction("Index").Mensagem("O cartão " + Cartao.Nome + " foi salvo com sucesso!", "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.SUCCESS), "add_alert");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.DANGER), "error");
            }
        }

        [HttpPost]
        public ActionResult SalvarFatura(FaturaModel Fatura)
        {
            try
            {
                FaturaRepository.Salvar(Fatura);
                return RedirectToAction("Detalhes", "Cartao", new { id = Fatura.IdCartao }).Mensagem("A fatura de " + Fatura.DataVencimento.ToString("MMMM") + " foi salva com sucesso!", "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.SUCCESS), "add_alert");
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Cartao", new { id = Fatura.IdCartao }).Mensagem(e.Message, "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.DANGER), "error");
            }
        }

        [HttpGet]
        public ActionResult Detalhes(long id)
        {
            try
            {
                CartaoModel Cartao = CartaoRepository.RecuperarPeloId(id);
                List<FaturaModel> Faturas = FaturaRepository.ListAllByCartao(id);
                ViewBag.Cartao = Cartao != null ? Cartao : new CartaoModel();
                ViewBag.Faturas = Faturas != null ? Faturas : new List<FaturaModel>();
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.DANGER), "error");
            }
        }

        [HttpGet]
        public ActionResult Remover(long id)
        {
            try
            {
                CartaoRepository.Remover(id);
                return RedirectToAction("Index").Mensagem("O cartão de id " + id + " foi removido com sucesso!", "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.INFO), "add_alert");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.DANGER), "error");
            }
        }

        [HttpGet]
        public ActionResult RemoverFatura(long Id, long IdCartao)
        {
            try
            {
                FaturaRepository.Remover(Id);
                return RedirectToAction("Detalhes", "Cartao", new { id = IdCartao }).Mensagem("A fatura de id " + Id + " foi Removida com sucesso!", "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.INFO), "add_alert");
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Cartao", new { id = IdCartao }).Mensagem(e.Message, "", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.DANGER), "error");
            }
        }
    }
}