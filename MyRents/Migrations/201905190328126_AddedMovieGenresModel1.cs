namespace MyRents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMovieGenresModel1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Movies", name: "Genre_Id", newName: "MovieGenreId");
            RenameIndex(table: "dbo.Movies", name: "IX_Genre_Id", newName: "IX_MovieGenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Movies", name: "IX_MovieGenreId", newName: "IX_Genre_Id");
            RenameColumn(table: "dbo.Movies", name: "MovieGenreId", newName: "Genre_Id");
        }
    }
}
