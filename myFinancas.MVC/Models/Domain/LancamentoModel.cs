using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class LancamentoModel : EntityModel
    {
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public bool IsParcelado { get; set; }

        [Display(Name = "Total de Parcelas")]
        public int QtdParcelas { get; set; }

        [Display(Name = "Parcela Atual")]
        public int ParcelaAtual { get; set; }

        [Display(Name = "Data da Compra")]
        public DateTime DataCompra { get; set; }
        public long IdFatura { get; set; }
        public virtual FaturaModel Fatura { get; set; }
        public long IdComprador { get; set; }
        public virtual CompradorModel Comprador { get; set; }

        public LancamentoModel()
        {
            this.ParcelaAtual = 1;
        }
    }
}