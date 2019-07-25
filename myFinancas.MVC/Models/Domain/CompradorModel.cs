using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Models.Domain
{
    public class CompradorModel : EntityModel
    {
        [Remote("ValidarNome", "Comprador", ErrorMessage = "Nome já Cadastrado.")]
        public string Nome { get; set; }

        [Display(Name = "Divida Total")]
        public Decimal DividaTotal { get; set; }

        [Display(Name = "Divida Paga")]
        public Decimal DividaTotalPaga { get; set; }

        [Display(Name = "Divida Restante")]
        public Decimal DividaTotalRestante { get; set; }


        public void VerificarDivida()
        {
            if (this.DividaTotalPaga >= this.DividaTotal)
            {
                this.DividaTotal = 0;
                this.DividaTotalPaga = 0;
            }
        }

        public void CalcularValorRestante()
        {
            this.DividaTotalRestante = this.DividaTotalPaga - this.DividaTotal;
        }
    }
}