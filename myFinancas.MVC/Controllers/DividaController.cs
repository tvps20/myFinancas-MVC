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
    public class DividaController : Controller
    {
        private DividaService dividaService = new DividaService(DividaRepository.getInstance());
        private CompradorService compradorService = new CompradorService(CompradorRepository.getInstance());
        // GET: Divida
        public ActionResult Index()
        {
            ViewBag.Compradores = this.compradorService.ListarTodos();
            ViewBag.Dividas = this.dividaService.ListarTodosIncludeComprador();
            return View();
        }

        [HttpPost]
        public ActionResult Salvar(DividaModel Divida)
        {
            try
            {
                Divida.CalcularValorRestante();
                this.dividaService.Salvar(Divida);
                return RedirectToAction("Index").Mensagem("A divida de " + Divida.ValorDivida.ToString("C") + " foi salva com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult Detalhes(long id)
        {
            DividaModel Divida = new DividaModel();

            if (id != 0) { Divida = this.dividaService.RecuperarPeloId(id); }
            ViewBag.Divida = Divida;

            return PartialView();
        }

        [HttpGet]
        public ActionResult Remover(long id)
        {
            try
            {
                this.dividaService.Remover(id);
                return RedirectToAction("Index").Mensagem("A divida de id " + id + " foi removida com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }
    }
}