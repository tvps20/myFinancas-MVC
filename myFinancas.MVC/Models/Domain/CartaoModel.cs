using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFinancas.MVC.Models.Domain
{
    public class CartaoModel : EntityModel
    {
        public string Bandeira { get; set; }

        [Remote("ValidarNome", "Cartao", ErrorMessage = "Nome já Cadastrado.")]
        public string Nome { get; set; }
    }
}