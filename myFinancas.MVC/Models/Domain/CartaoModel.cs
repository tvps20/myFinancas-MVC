using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class CartaoModel : EntityModel
    {
        public string Bandeira { get; set; }
        public string Nome { get; set; }
    }
}