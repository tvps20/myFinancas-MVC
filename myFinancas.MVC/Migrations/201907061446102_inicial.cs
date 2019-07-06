namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cartoes",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        bandeira = c.String(nullable: false, maxLength: 15),
                        nome = c.String(nullable: false, maxLength: 30),
                        created_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(nullable: false),
                        is_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.faturas",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        data_vencimento = c.DateTime(nullable: false),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        observacao = c.String(),
                        is_paga = c.Boolean(nullable: false),
                        is_fechada = c.Boolean(nullable: false),
                        id_cartao = c.Long(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(nullable: false),
                        is_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.cartoes", t => t.id_cartao, cascadeDelete: true)
                .Index(t => t.id_cartao);
            
            CreateTable(
                "dbo.lancamentos",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        descricao = c.String(nullable: false),
                        observacao = c.String(),
                        is_parcelado = c.Boolean(nullable: false),
                        qtd_parcelas = c.Int(nullable: false),
                        parcela_atual = c.Int(nullable: false),
                        DataCompra = c.DateTime(nullable: false),
                        id_fatura = c.Long(nullable: false),
                        id_comprador = c.Long(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(nullable: false),
                        is_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.faturas", t => t.id_fatura, cascadeDelete: true)
                .ForeignKey("dbo.compradores", t => t.id_comprador, cascadeDelete: true)
                .Index(t => t.id_fatura)
                .Index(t => t.id_comprador);
            
            CreateTable(
                "dbo.compradores",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        nome = c.String(),
                        divida_total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        divida_total_paga = c.Decimal(nullable: false, precision: 18, scale: 2),
                        divida_total_restante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        created_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(nullable: false),
                        is_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.dividas",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        data = c.DateTime(nullable: false),
                        valor_divida = c.Decimal(nullable: false, precision: 18, scale: 2),
                        valor_pago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        valor_restante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        observacao = c.String(),
                        is_paga = c.Boolean(nullable: false),
                        id_comprador = c.Long(nullable: false),
                        id_fatura = c.Long(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        update_at = c.DateTime(nullable: false),
                        is_ativo = c.Boolean(nullable: false),
                        CompradorModel_Id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.compradores", t => t.id_comprador, cascadeDelete: true)
                .ForeignKey("dbo.faturas", t => t.id_fatura, cascadeDelete: true)
                .ForeignKey("dbo.compradores", t => t.CompradorModel_Id)
                .Index(t => t.id_comprador)
                .Index(t => t.id_fatura)
                .Index(t => t.CompradorModel_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.dividas", new[] { "CompradorModel_Id" });
            DropIndex("dbo.dividas", new[] { "id_fatura" });
            DropIndex("dbo.dividas", new[] { "id_comprador" });
            DropIndex("dbo.lancamentos", new[] { "id_comprador" });
            DropIndex("dbo.lancamentos", new[] { "id_fatura" });
            DropIndex("dbo.faturas", new[] { "id_cartao" });
            DropForeignKey("dbo.dividas", "CompradorModel_Id", "dbo.compradores");
            DropForeignKey("dbo.dividas", "id_fatura", "dbo.faturas");
            DropForeignKey("dbo.dividas", "id_comprador", "dbo.compradores");
            DropForeignKey("dbo.lancamentos", "id_comprador", "dbo.compradores");
            DropForeignKey("dbo.lancamentos", "id_fatura", "dbo.faturas");
            DropForeignKey("dbo.faturas", "id_cartao", "dbo.cartoes");
            DropTable("dbo.dividas");
            DropTable("dbo.compradores");
            DropTable("dbo.lancamentos");
            DropTable("dbo.faturas");
            DropTable("dbo.cartoes");
        }
    }
}
