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
using PagedList;
using System.Transactions;

namespace myFinancas.MVC.Controllers
{
    public class DividaController : Controller
    {
        private DividaService dividaService = new DividaService(DividaRepository.getInstance());
        private CompradorService compradorService = new CompradorService(CompradorRepository.getInstance());
        // GET: Divida
        public ActionResult Index(int pagina = 1)
        {
            try
            {
                pagina = pagina == 0 ? 1 : pagina;
                int qtdElementos = 10;
                IPagedList<DividaModel> dividas = this.dividaService.ListarTodosIncludeComprador().ToPagedList(pagina, qtdElementos);

                ViewBag.active = "Divida";
                ViewBag.Compradores = this.compradorService.ListarTodos();
                ViewBag.PagedList = dividas;
                ViewBag.PageNumber = CustomUtil<DividaModel>.caculaNumeroPagina(pagina, dividas);
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Dashboard").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpPost]
        public ActionResult Salvar(DividaModel Divida)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    Divida.CalcularValorRestante();
                    this.dividaService.Salvar(Divida);
                    CompradorModel comprador = this.compradorService.RecuperarPeloId(Divida.IdComprador);
                    comprador.DividaTotal += Divida.ValorDivida;
                    comprador.DividaTotalPaga += Divida.ValorPago;
                    comprador.CalcularValorRestante();
                    comprador.VerificarDivida();
                    this.compradorService.Salvar(comprador);

                    transaction.Complete();
                    return RedirectToAction("Index").Mensagem("A divida de " + Divida.ValorDivida.ToString("C") + " foi salva com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
                }
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
                DividaModel Divida = new DividaModel();

                if (id != 0) { Divida = this.dividaService.RecuperarPeloId(id); }
                ViewBag.Divida = Divida;
                ViewBag.active = "Divida";

                return PartialView();
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