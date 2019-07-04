using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public enum TipoIcone
    {
        [Description("done")]
        SUCESSO = 1,
        [Description("error")]
        ERRO = 0,
        [Description("info")]
        INFO = 1

    }
}