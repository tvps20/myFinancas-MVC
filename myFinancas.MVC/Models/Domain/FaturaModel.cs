using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class FaturaModel : EntityModel
    {
        [Display(Name = "Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        // Mes referente da fatura. Salvo no fomato MMMM/yyyy
        public string MesReferente { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }
        public String Observacao { get; set; }
        public bool IsPaga { get; set; }
        public bool IsFechada { get; set; }
        public long IdCartao { get; set; }
        public virtual CartaoModel Cartao { get; set; }
    }
}