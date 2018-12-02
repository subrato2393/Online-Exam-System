namespace OnlineExam.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_the_Type_CourseTag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "CourseDuration", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "CourseDuration", c => c.DateTime(nullable: false));
        }
    }
}
