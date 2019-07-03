using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Maps
{
    public class FaturaMap : EntityTypeConfiguration<FaturaModel>
    {
        public FaturaMap()
        {
            ToTable("faturas");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedAt).HasColumnName("created_at");
            Property(x => x.UpdateAt).HasColumnName("update_at");
            Property(x => x.IsAtivo).HasColumnName("is_ativo");

            Property(x => x.DataVencimento).HasColumnName("data_vencimento").IsRequired();
            Property(x => x.Valor).HasColumnName("valor");
            Property(x => x.Observacao).HasColumnName("observacao");
            Property(x => x.IsPaga).HasColumnName("is_paga");
            Property(x => x.IsFechada).HasColumnName("is_fechada");

            Property(x => x.IdCartao).HasColumnName("id_cartao").IsRequired();
            HasRequired(x => x.Cartao).WithMany().HasForeignKey(x => x.IdCartao);
        }
    }
}