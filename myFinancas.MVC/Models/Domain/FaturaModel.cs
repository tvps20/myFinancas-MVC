using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class FaturaModel : EntityModel
    {
        public DateTime DataVencimento { get; set; }
        public String Observacao { get; set; }
        public bool IsPaga { get; set; }
        public bool isFechada { get; set; }
        public long IdCartao { get; set; }
        public virtual CartaoModel Cartao { get; set; }
    }
}