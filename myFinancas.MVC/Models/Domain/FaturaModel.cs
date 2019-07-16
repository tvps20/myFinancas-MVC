using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Models.Domain
{
    public class FaturaModel : EntityModel
    {
        [Display(Name = "Data de Vencimento")]
        [Required(ErrorMessage = "O vencimento é obrigatório")]
        public DateTime DataVencimento { get; set; }

        // Mes referente da fatura. Salvo no fomato MMMM/yyyy
        [Display(Name = "Mês Referente")]
        [Required(ErrorMessage = "O mês é obrigatório")]
        [Remote("ValidarFatura", "Fatura", ErrorMessage = "Fatura já Cadastrado.", AdditionalFields = "IdCartao")]
        public string MesReferente { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }
        public string Observacao { get; set; }

        [Display(Name = "Fatura Paga")]
        public bool IsPaga { get; set; }

        [Display(Name = "Fatura Fechada")]
        public bool IsFechada { get; set; }
        public long IdCartao { get; set; }
        public virtual CartaoModel Cartao { get; set; }
    }
}