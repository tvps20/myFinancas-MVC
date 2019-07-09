using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class DividaModel : EntityModel
    {
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal ValorDivida { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal ValorPago { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal ValorRestante { get; set; }
        public string Descricao { get; set; }
        public bool isPaga { get; set; }
        public long IdComprador { get; set; }
        public virtual CompradorModel Comprador { get; set; }
        public long? IdFatura { get; set; }
        public virtual FaturaModel Fatura { get; set; }
    }
}