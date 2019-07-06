using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Maps
{
    public class DividaMap : EntityTypeConfiguration<DividaModel>
    {
        public DividaMap()
        {
            ToTable("dividas");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedAt).HasColumnName("created_at");
            Property(x => x.UpdateAt).HasColumnName("update_at");
            Property(x => x.IsAtivo).HasColumnName("is_ativo");

            Property(x => x.Data).HasColumnName("data").IsRequired();
            Property(x => x.ValorDivida).HasColumnName("valor_divida");
            Property(x => x.ValorPago).HasColumnName("valor_pago");
            Property(x => x.ValorRestante).HasColumnName("valor_restante");
            Property(x => x.Observacao).HasColumnName("observacao");
            Property(x => x.isPaga).HasColumnName("is_paga");

            Property(x => x.IdComprador).HasColumnName("id_comprador").IsRequired();
            HasRequired(x => x.Comprador).WithMany().HasForeignKey(x => x.IdComprador);

            Property(x => x.IdFatura).HasColumnName("id_fatura").IsOptional();
            HasOptional(x => x.Fatura).WithMany().HasForeignKey(x => x.IdFatura);
        }
    }
}