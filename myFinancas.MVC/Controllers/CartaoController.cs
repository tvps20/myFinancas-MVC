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
            ViewBag.Cartoes = CartaoRepository.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult Salvar(CartaoModel Cartao)
        {
            try
            {
                CartaoRepository.Salvar(Cartao);
                return RedirectToAction("Index").Mensagem("O cartão " + Cartao.Nome + " foi salvo com sucesso!","", EnumExtensions.TipoPontoToDescriptionString(TipoMensagem.SUCCESS), "add_alert");
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
    }
}