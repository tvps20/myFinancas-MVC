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
    public class CompradorController : Controller
    {
        // GET: Comprador
        public ActionResult Index()
        {
            try
            {
                List<CompradorModel> Compradores = CompradorRepository.ListAll();
                ViewBag.Compradores = Compradores != null ? Compradores : new List<CompradorModel>();
                return View("Index");
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
                CompradorRepository.Salvar(Comprador);
                return RedirectToAction("Index").Mensagem("O comprador " + Comprador.Nome + " foi salvo com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.SUCCESS), EnumExtensions.EnumToDescriptionString(TipoIcone.SUCESSO));
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
                CompradorRepository.Remover(id);
                return RedirectToAction("Index").Mensagem("O comprador de id " + id + " foi removido com sucesso!", "", EnumExtensions.EnumToDescriptionString(TipoMensagem.INFO), EnumExtensions.EnumToDescriptionString(TipoIcone.INFO));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Mensagem(e.Message, "", EnumExtensions.EnumToDescriptionString(TipoMensagem.DANGER), EnumExtensions.EnumToDescriptionString(TipoIcone.ERRO));
            }
        }
    }
}