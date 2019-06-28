using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Maps
{
    public class CartaoMap : EntityTypeConfiguration<CartaoModel>
    {
        public CartaoMap()
        {
            ToTable("cartoes");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedAt).HasColumnName("created_at");
            Property(x => x.UpdateAt).HasColumnName("update_at");
            Property(x => x.Ativo).HasColumnName("ativo");
            Property(x => x.Nome).HasMaxLength(30).HasColumnName("nome").IsRequired();
            Property(x => x.Bandeira).HasMaxLength(15).HasColumnName("bandeira").IsRequired();
        }
    }
}