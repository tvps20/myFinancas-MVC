namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lancamentos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.lancamentos",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        descricao = c.String(),
                        observacao = c.String(),
                        is_parcelado = c.Boolean(nullable: false),
                        is_pago = c.Boolean(nullable: false),
                        qtd_parcelas = c.Int(nullable: false),
                        parcela_atual = c.Int(nullable: false),
                        DataCompra = c.DateTime(nullable: false),
                        id_fatura = c.Long(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(nullable: false),
                        is_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.faturas", t => t.id_fatura, cascadeDelete: true)
                .Index(t => t.id_fatura);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.lancamentos", new[] { "id_fatura" });
            DropForeignKey("dbo.lancamentos", "id_fatura", "dbo.faturas");
            DropTable("dbo.lancamentos");
        }
    }
}
