using System.Data.Entity.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddRatingColumnToMovieTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Rating", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Rating");
        }
    }
}
