using myFinancas.MVC.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Util
{
    public static class ActionResultExtensions
    {
        /// <summary>
        /// Redireciona para uma ActionResult retornando uma mensagem de confirmação para a View
        /// </summary>
        /// <param name="actionResult"></param>
        /// <param name="mensagem">Mensagem a ser exibida</param>
        /// <param name="titulo">titulo a ser exibido, sendo omitido apresenta defaut 'Atenção'</param>
        /// <returns></returns>
        public static ActionResult Mensagem(this ActionResult actionResult, string mensagem, string titulo = "Aviso", string tipo = "info", string icon= "info")
        {
            return new TempDataActionResult(actionResult, mensagem, titulo, tipo, icon);
        }
    }

    public class TempDataActionResult : ActionResult
    {
        private readonly ActionResult _actionResult;
        private readonly string _mensagem;
        private readonly string _titulo;
        private readonly string _tipo;
        private readonly string _icone;

        public TempDataActionResult(ActionResult actionResult, string Mensagem, string Titulo, string tipo, string icone)
        {
            _actionResult = actionResult;
            _mensagem = Mensagem;
            _titulo = Titulo;
            _tipo = tipo;
            _icone = icone;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData["Mensagem"] = _mensagem;
            context.Controller.TempData["Titulo"] = _titulo;
            context.Controller.TempData["Tipo"] = _tipo;
            context.Controller.TempData["Icone"] = _icone;
            _actionResult.ExecuteResult(context);
        }
    }
}