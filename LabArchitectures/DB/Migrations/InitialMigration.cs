﻿


namespace LabArchitectures.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Users", c => new
            {
                UserId = c.Guid(nullable: false),
                FirstName = c.String(nullable: false),
                LastName = c.String(nullable: false),
                Email = c.String(),
                Login = c.String(nullable: false),
                Password = c.String(nullable: false),
                LastLoginDate = c.DateTime(nullable: false),
            }).PrimaryKey(u => u.UserId);
            CreateTable("dbo.Queries", c => new
            {
                QueryId = c.Guid(nullable: false),
                UserId = c.Guid(nullable: false),
                FilePath = c.String(nullable: false),
                ExecDate = c.DateTime(nullable: false),
                WordCnt = c.Int(nullable: false),
                CharCnt = c.Int(nullable: false),
                LineCnt = c.Int(nullable: false)
            }).PrimaryKey(q => q.QueryId).ForeignKey("dbo.Users", q => q.UserId, cascadeDelete: true).Index(q => q.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Queries", "UserID", "dbo.Users");
            DropIndex("dbo.Queries", new[] { "UserID" });
            DropTable("dbo.Queries");
            DropTable("dbo.Users");
        }
    }

}