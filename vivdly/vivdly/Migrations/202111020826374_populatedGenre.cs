namespace vivdly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populatedGenre : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres(id, name) values(1, 'Comedy')");
            Sql("Insert into Genres(id, name) values(2, 'Action')");
            Sql("Insert into Genres(id, name) values(3, 'Family')");
            Sql("Insert into Genres(id, name) values(4, 'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
