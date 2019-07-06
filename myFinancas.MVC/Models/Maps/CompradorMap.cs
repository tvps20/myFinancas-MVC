using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Maps
{
    public class CompradorMap : EntityTypeConfiguration<CompradorModel>
    {
        public CompradorMap()
        {
            ToTable("compradores");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedAt).HasColumnName("created_at");
            Property(x => x.UpdateAt).HasColumnName("update_at");
            Property(x => x.IsAtivo).HasColumnName("is_ativo");

            Property(x => x.Nome).HasColumnName("nome");
            Property(x => x.DividaTotal).HasColumnName("divida_total");
            Property(x => x.DividaTotalPaga).HasColumnName("divida_total_paga");
            Property(x => x.DividaTotalRestante).HasColumnName("divida_total_restante");
        }
    }
}