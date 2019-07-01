using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public enum TipoMensagem
    {
        [Description("Alerta")]
        PRIMARY = 0,
        [Description("Informação")]
        INFO = 2,
        [Description("Sucesso")]
        SUCCESS = 3,
        [Description("Erro")]
        DANGER = 4,
        [Description("Aviso")]
        WARNING = 5,
        [Description("Atenção")]
        DEFAULT = 6
    }
}