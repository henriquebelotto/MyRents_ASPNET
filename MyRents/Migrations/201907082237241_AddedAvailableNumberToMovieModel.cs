namespace MyRents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAvailableNumberToMovieModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));

            // Seeding the NumberAvailable intially to the NumberInStock
            Sql("UPDATE movies SET NumberAvailable = NumberInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
