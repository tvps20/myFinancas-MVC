namespace myFinancas.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lancamentos3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.lancamentos", "is_pago");
        }
        
        public override void Down()
        {
            AddColumn("dbo.lancamentos", "is_pago", c => c.Boolean(nullable: false));
        }
    }
}
