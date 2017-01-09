namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DeleteCategoriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo._Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            Sql("insert into _Categories select Id, Name from Categories");
            DropTable("dbo.Categories");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            Sql("insert into Categories select Id, Name from _Categories");
            DropTable("dbo._Categories");
        }
    }
}
