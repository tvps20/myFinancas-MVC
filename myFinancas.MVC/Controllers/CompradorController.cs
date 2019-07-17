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
    public class CompradorController : Controller
    {
        private CompradorService compradorService = new CompradorService(CompradorRepository.getInstance());
        private DividaService dividaService = new DividaService(DividaRepository.getInstance());
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());
        // GET: Comprador
        public ActionResult Index()
        {
            try
            {
                ViewBag.active = "Compradores";
                ViewBag.Compradores = this.compradorService.ListarTodos();
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpPost]
        public ActionResult Salvar(CompradorModel Comprador)
        {
            try
            {
                this.compradorService.Salvar(Comprador);
                return RedirectToAction("Index").Mensagem("O comprador " + Comprador.Nome + " foi salvo com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult Detalhes(long id)
        {
            try
            {
                ViewBag.active = "Compradores";
                Dictionary<string, List<LancamentoModel>> Lancamentos = this.lancamentoService.ListarTodosLancamentosCompradorNPagos(id);
                ViewBag.Comprador = this.compradorService.RecuperarPeloId(id);
                ViewBag.Dividas = this.dividaService.ListarTodasDividasCompradorNPagos(id);
                ViewBag.Lancamentos = Lancamentos;
                ViewBag.DividasCount = this.compradorService.ContaLancamentos(Lancamentos);
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
                this.compradorService.Remover(id);
                return RedirectToAction("Index").Mensagem("O comprador de id " + id + " foi removido com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        // Validando o campo Nome da model Cartão
        public ActionResult ValidarNome(string Nome)
        {
            CompradorModel comprador = this.compradorService.BuscarPeloNome(Nome);

            if (comprador != null)
            {
                return Json(string.Format("O Nome '{0}' já esta cadastrado.", Nome), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}