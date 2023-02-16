namespace SMS.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestMetaAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestMeta",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ModuleName = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        ModuleId = c.Guid(),
                        SchoolId = c.Guid(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.String(),
                        DeletedDate = c.DateTime(),
                        DeletedBy = c.String(),
                        IsDeleted = c.Boolean(),
                        ApprovalStatus = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            AddColumn("dbo.AspNetUsers", "ApprovalStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestMeta", "SchoolId", "dbo.Schools");
            DropIndex("dbo.RequestMeta", new[] { "SchoolId" });
            DropColumn("dbo.AspNetUsers", "ApprovalStatus");
            DropTable("dbo.RequestMeta");
        }
    }
}
