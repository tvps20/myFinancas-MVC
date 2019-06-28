using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Domain
{
    public abstract class EntityModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Ativo { get; set; }


        public EntityModel()
        {
            this.Ativo = true;
        }
    }
}