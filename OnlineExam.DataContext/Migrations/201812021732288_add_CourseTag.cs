namespace OnlineExam.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_CourseTag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseTags", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseTags", "TagId", "dbo.Tags");
            DropIndex("dbo.CourseTags", new[] { "TagId" });
            DropIndex("dbo.CourseTags", new[] { "CourseId" });
            AddColumn("dbo.Courses", "CourseTag", c => c.String());
            DropTable("dbo.CourseTags");
            DropTable("dbo.Tags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Courses", "CourseTag");
            CreateIndex("dbo.CourseTags", "CourseId");
            CreateIndex("dbo.CourseTags", "TagId");
            AddForeignKey("dbo.CourseTags", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourseTags", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
