﻿using myFinancas.MVC.Models.Domain;
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
    public class CartaoController : Controller
    {
        private CartaoService cartaoService = new CartaoService(CartaoRepository.getInstance());
        private FaturaService faturaService = new FaturaService(FaturaRepository.getInstance());
        // GET: Cartao
        public ActionResult Index()
        {
            try
            {
                ViewBag.Cartoes = this.cartaoService.ListarTodos();
                return View("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpPost]
        public ActionResult Salvar(CartaoModel Cartao)
        {
            try
            {
                this.cartaoService.Salvar(Cartao);
                return RedirectToAction("Index").Mensagem("O cartão " + Cartao.Nome + " foi salvo com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpPost]
        public ActionResult SalvarFatura(FaturaModel Fatura)
        {
            try
            {
                this.faturaService.Salvar(Fatura);
                return RedirectToAction("Detalhes", "Cartao", new { id = Fatura.IdCartao }).Mensagem("A fatura de " + Fatura.DataVencimento.ToString("MMMM") + " foi salva com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Cartao", new { id = Fatura.IdCartao }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult Detalhes(long id)
        {
            try
            {
                ViewBag.Cartao = this.cartaoService.RecuperarPeloId(id);
                ViewBag.Faturas = this.faturaService.ListarTodosPeloCartao(id);
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
                this.cartaoService.Remover(id);
                return RedirectToAction("Index").Mensagem("O cartão de id " + id + " foi removido com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult RemoverFatura(long Id, long IdCartao)
        {
            try
            {
                this.faturaService.Remover(Id);
                return RedirectToAction("Detalhes", "Cartao", new { id = IdCartao }).Mensagem("A fatura de id " + Id + " foi Removida com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Cartao", new { id = IdCartao }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }
    }
}