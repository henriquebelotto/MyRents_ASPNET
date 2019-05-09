namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovieGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into MovieGenres (Id, Name) VALUES (1, 'Commedy')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (2, 'Romance')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (3, 'Action')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (4, 'Adventure')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (5, 'Crime')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (6, 'Horror')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (7, 'Science Fiction')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (8, 'War')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (9, 'Western')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (10, 'Romance')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (11, 'Thriller-Suspense')");
            Sql("Insert into MovieGenres (Id, Name) VALUES (12, 'Documentary')");
        }
        
        public override void Down()
        {
        }
    }
}
