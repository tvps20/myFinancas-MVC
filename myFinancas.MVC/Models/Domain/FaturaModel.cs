using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class FaturaModel : EntityModel
    {
        [Description("Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        public String Observacao { get; set; }

        [Description("Paga")]
        public bool IsPaga { get; set; }

        [Description("Fechada")]
        public bool IsFechada { get; set; }
        public long IdCartao { get; set; }
        public virtual CartaoModel Cartao { get; set; }
    }
}