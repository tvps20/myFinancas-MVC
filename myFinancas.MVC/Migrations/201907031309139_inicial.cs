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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.faturas", new[] { "id_cartao" });
            DropForeignKey("dbo.faturas", "id_cartao", "dbo.cartoes");
            DropTable("dbo.faturas");
            DropTable("dbo.cartoes");
        }
    }
}
