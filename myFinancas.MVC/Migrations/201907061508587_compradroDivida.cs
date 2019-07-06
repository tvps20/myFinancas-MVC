namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compradroDivida : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dividas", "CompradorModel_Id", c => c.Long());
            AddForeignKey("dbo.dividas", "CompradorModel_Id", "dbo.compradores", "id");
            CreateIndex("dbo.dividas", "CompradorModel_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.dividas", new[] { "CompradorModel_Id" });
            DropForeignKey("dbo.dividas", "CompradorModel_Id", "dbo.compradores");
            DropColumn("dbo.dividas", "CompradorModel_Id");
        }
    }
}
