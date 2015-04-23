namespace Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FizzBuzzs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(),
                        Message = c.String(),
                        DateTimeEntered = c.DateTime(),
                        Active = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SetUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Divisor = c.String(),
                        Message = c.String(),
                        DateTimeEntered = c.DateTime(),
                        Active = c.Byte(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SetUps");
            DropTable("dbo.FizzBuzzs");
        }
    }
}
