using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public class LancamentoModel : EntityModel
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public bool IsParcelado { get; set; }
        public int QtdParcelas { get; set; }
        public int ParcelaAtual { get; set; }
        public DateTime DataCompra { get; set; }
        public long IdFatura { get; set; }
        public virtual FaturaModel Fatura { get; set; }

        public LancamentoModel()
        {
            this.ParcelaAtual = 1;
        }
    }
}