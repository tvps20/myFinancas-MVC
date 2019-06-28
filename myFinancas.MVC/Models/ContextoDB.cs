using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Models.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models
{
    public class ContextoDB : DbContext
    {
        public ContextoDB() : base("name=principal") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CartaoMap());
        }

        public DbSet<CartaoModel> Cartoes { get; set; }
    }
}