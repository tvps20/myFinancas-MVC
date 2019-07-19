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

namespace myFinancas.MVC.Controllers
{
    public class CartaoController : Controller
    {
        private CartaoService cartaoService = new CartaoService(CartaoRepository.getInstance());
        private FaturaService faturaService = new FaturaService(FaturaRepository.getInstance());
        // GET: Cartao
        public ActionResult Index(int pagina = 1)
        {
            try
            {
                pagina = pagina == 0 ? 1 : pagina;
                int qtdElementos = 10;
                IPagedList<CartaoModel> cartoes = this.cartaoService.ListarTodos().ToPagedList(pagina, qtdElementos);

                ViewBag.active = "Cartao";
                ViewBag.PagedList = cartoes;
                ViewBag.PageNumber = CustomUtil<CartaoModel>.caculaNumeroPagina(pagina, cartoes);
                return View();
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
                Fatura.MesReferente += Fatura.DataVencimento.ToString("'/'yyyy");
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
                ViewBag.active = "Cartao";
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

        // Validando o campo Nome da model Cartão
        public ActionResult ValidarNome(string Nome)
        {
            CartaoModel cartao = this.cartaoService.BuscarPeloNome(Nome);

            if(cartao != null)
            {
                return Json(string.Format("O Nome '{0}' já esta cadastrado.", Nome), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PagarFatura(long Id, long IdCartao)
        {
            try
            {
                this.faturaService.PagarFatura(Id);
                return RedirectToAction("Detalhes", new { id = IdCartao }).Mensagem("A fatura de id " + Id + " foi paga com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", new { id = IdCartao }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult FecharFatura(long Id, long IdCartao)
        {
            try
            {
                this.faturaService.FecharFatura(Id);
                return RedirectToAction("Detalhes", new { id = IdCartao }).Mensagem("A fatura de id " + Id + " foi fechada com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", new { id = IdCartao }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }
    }
}