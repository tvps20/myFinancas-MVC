using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class CompradorModel : EntityModel
    {
        public string Nome { get; set; }

        [Display(Name = "Divida Total")]
        public Decimal DividaTotal { get; set; }

        [Display(Name = "Divida Paga")]
        public Decimal DividaTotalPaga { get; set; }

        [Display(Name = "Divida Restante")]
        public Decimal DividaTotalRestante { get; set; }
    }
}