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
            modelBuilder.Configurations.Add(new FaturaMap());
        }

        public DbSet<CartaoModel> Cartoes { get; set; }
        public DbSet<FaturaModel> Faturas { get; set; }
    }
}