namespace Web11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Theme_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        ParentComment_Id = c.Int(nullable: false),
                        Content = c.String(),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        Edited = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentComment_Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.ParentComment_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        RegistrationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubForum_Id = c.Int(nullable: false),
                        Title = c.String(),
                        Author_Id = c.Int(nullable: false),
                        Text = c.String(),
                        Image = c.String(),
                        Link = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: false)
                .ForeignKey("dbo.SubForums", t => t.SubForum_Id, cascadeDelete: true)
                .Index(t => t.SubForum_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.SubForums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        ResponsibleModerator_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ResponsibleModerator_Id, cascadeDelete: false)
                .Index(t => t.ResponsibleModerator_Id);
            
            CreateTable(
                "dbo.FollowSubForums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        SubForum_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubForums", t => t.SubForum_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.SubForum_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sender_Id = c.Int(nullable: false),
                        Receiver_Id = c.Int(nullable: false),
                        Text = c.String(),
                        Readed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_Id, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.Sender_Id, cascadeDelete: false)
                .Index(t => t.Sender_Id)
                .Index(t => t.Receiver_Id);
            
            CreateTable(
                "dbo.SavedComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Comment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.SavedThemes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Theme_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Theme_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedThemes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SavedThemes", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.SavedComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SavedComments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Users");
            DropForeignKey("dbo.FollowSubForums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FollowSubForums", "SubForum_Id", "dbo.SubForums");
            DropForeignKey("dbo.Comments", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Themes", "SubForum_Id", "dbo.SubForums");
            DropForeignKey("dbo.SubForums", "ResponsibleModerator_Id", "dbo.Users");
            DropForeignKey("dbo.Themes", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropIndex("dbo.SavedThemes", new[] { "Theme_Id" });
            DropIndex("dbo.SavedThemes", new[] { "User_Id" });
            DropIndex("dbo.SavedComments", new[] { "Comment_Id" });
            DropIndex("dbo.SavedComments", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.FollowSubForums", new[] { "SubForum_Id" });
            DropIndex("dbo.FollowSubForums", new[] { "User_Id" });
            DropIndex("dbo.SubForums", new[] { "ResponsibleModerator_Id" });
            DropIndex("dbo.Themes", new[] { "Author_Id" });
            DropIndex("dbo.Themes", new[] { "SubForum_Id" });
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Theme_Id" });
            DropTable("dbo.SavedThemes");
            DropTable("dbo.SavedComments");
            DropTable("dbo.Messages");
            DropTable("dbo.FollowSubForums");
            DropTable("dbo.SubForums");
            DropTable("dbo.Themes");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}