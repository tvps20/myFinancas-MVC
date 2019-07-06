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
            modelBuilder.Configurations.Add(new LancamentoMap());
            modelBuilder.Configurations.Add(new CompradorMap());
            modelBuilder.Configurations.Add(new DividaMap());
        }

        public DbSet<CartaoModel> Cartoes { get; set; }
        public DbSet<FaturaModel> Faturas { get; set; }
        public DbSet<LancamentoModel> Lancamentos { get; set; }
        public DbSet<CompradorModel> Compradores { get; set; }
        public DbSet<DividaModel> Dividas { get; set; }
    }
}