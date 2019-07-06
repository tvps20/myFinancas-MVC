using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class CompradorModel : EntityModel
    {
        public string Nome { get; set; }
        public virtual List<DividaModel> Dividas { get; set; }
    }
}