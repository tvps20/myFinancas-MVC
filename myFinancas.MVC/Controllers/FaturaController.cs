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
                Fatura.MesReferente += Fatura.DataVencimento.ToString("'/'yyyy");
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
                FaturaModel fatura = this.faturaService.RecuperarPeloId(Lancamento.IdFatura);
                fatura.Valor += Lancamento.Valor;
                this.lancamentoService.Salvar(Lancamento);
                this.faturaService.Salvar(fatura);
                return RedirectToAction("Detalhes", "Fatura", new { id = Lancamento.IdFatura }).Mensagem("O lancamento de R$ " + Lancamento.Valor.ToString("C") + " foi salvo com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
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
                List<LancamentoModel> lancamentos = this.lancamentoService.ListarTodosPelaFaturaIncludeComprador(id);
                ViewBag.Fatura = this.faturaService.RecuperarPeloId(id);
                ViewBag.Lancamentos = lancamentos;
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
                LancamentoModel lancamento = this.lancamentoService.RecuperarPeloId(Id);
                FaturaModel fatura = this.faturaService.RecuperarPeloId(IdFatura);
                fatura.Valor -= lancamento.Valor;
                this.lancamentoService.Remover(Id);
                this.faturaService.Salvar(fatura);
                return RedirectToAction("Detalhes", "Fatura", new { id = IdFatura }).Mensagem("O lancamento de id " + Id + " foi Removido com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Detalhes", "Fatura", new { id = IdFatura }).Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult PagarFatura(long Id)
        {
            try
            {
                this.faturaService.PagarFatura(Id);
                return RedirectToAction("Index").Mensagem("A fatura de id " + Id + " foi paga com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        [HttpGet]
        public ActionResult FecharFatura(long Id)
        {
            try
            {
                this.faturaService.FecharFatura(Id);
                return RedirectToAction("Index").Mensagem("A fatura de id " + Id + " foi fechada com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }

        // Validando o campo Mes da model Cartão
        public ActionResult ValidarFatura(string MesReferente, long IdCartao)
        {
            MesReferente += DateTime.Now.ToString("'/'yyyy");
            FaturaModel fatura = this.faturaService.BuscarPeloMes(MesReferente, IdCartao);

            if (fatura != null)
            {
                return Json(string.Format("Já existe uma fatura de '{0}'.", MesReferente.Split('/')[0]), JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}