using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public enum TipoMensagem
    {
        [Description("primary")]
        PRIMARY = 0,
        [Description("info")]
        INFO = 2,
        [Description("success")]
        SUCCESS = 3,
        [Description("danger")]
        DANGER = 4,
        [Description("warning")]
        WARNING = 5,
        [Description("default")]
        DEFAULT = 6
    }
}