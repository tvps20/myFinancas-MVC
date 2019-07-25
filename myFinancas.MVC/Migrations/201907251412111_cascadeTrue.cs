namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadeTrue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.dividas", "id_fatura", "dbo.faturas");
            DropIndex("dbo.dividas", new[] { "id_fatura" });
            AddForeignKey("dbo.dividas", "id_fatura", "dbo.faturas", "id", cascadeDelete: true);
            CreateIndex("dbo.dividas", "id_fatura");
        }
        
        public override void Down()
        {
            DropIndex("dbo.dividas", new[] { "id_fatura" });
            DropForeignKey("dbo.dividas", "id_fatura", "dbo.faturas");
            CreateIndex("dbo.dividas", "id_fatura");
            AddForeignKey("dbo.dividas", "id_fatura", "dbo.faturas", "id");
        }
    }
}
