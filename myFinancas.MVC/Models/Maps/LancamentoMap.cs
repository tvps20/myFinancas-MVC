using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Maps
{
    public class LancamentoMap : EntityTypeConfiguration<LancamentoModel>
    {
        public LancamentoMap()
        {
            ToTable("lancamentos");

            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedAt).HasColumnName("created_at");
            Property(x => x.UpdateAt).HasColumnName("update_at");
            Property(x => x.IsAtivo).HasColumnName("is_ativo");

            Property(x => x.Valor).HasColumnName("valor").IsRequired();
            Property(x => x.Descricao).HasColumnName("descricao").IsRequired();
            Property(x => x.Observacao).HasColumnName("observacao");
            Property(x => x.IsParcelado).HasColumnName("is_parcelado").IsRequired();
            Property(x => x.QtdParcelas).HasColumnName("qtd_parcelas");
            Property(x => x.ParcelaAtual).HasColumnName("parcela_atual");

            Property(x => x.IdFatura).HasColumnName("id_fatura").IsRequired();
            HasRequired(x => x.Fatura).WithMany().HasForeignKey(x => x.IdFatura);
        }
    }
}