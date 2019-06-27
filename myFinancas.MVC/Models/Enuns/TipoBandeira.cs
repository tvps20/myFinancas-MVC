using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public enum TipoBandeira
    {
        [Description("Mastercard")]
        MASTERCARD = 0,

        [Description("Maestro")]
        MAESTRO = 1,

        [Description("Visa")]
        VISA = 2
    }
}