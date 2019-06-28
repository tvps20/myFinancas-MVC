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
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.cartoes");
        }
    }
}
