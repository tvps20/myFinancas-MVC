using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public enum TipoMes
    {
        [Description("Janeiro")]
        JANEIRO = 1,
        [Description("Fevereiro")]
        FEVEREIRO = 2,
        [Description("Março")]
        MARCO = 3,
        [Description("Abril")]
        ABRIL = 4,
        [Description("Maio")]
        MAIO = 5,
        [Description("Junho")]
        JUNHO = 6,
        [Description("Julho")]
        JULHO = 7,
        [Description("Agosto")]
        AGOSTO = 8,
        [Description("Setembro")]
        SETEMBRO = 9,
        [Description("Outubro")]
        OUTUBRO = 10,
        [Description("Novembro")]
        NOVEMBRO = 11,
        [Description("Dezembro")]
        DEZEMBRO = 12
    }
}