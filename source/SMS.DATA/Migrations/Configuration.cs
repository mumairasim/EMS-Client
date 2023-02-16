using System.Data.Entity.Migrations;

namespace SMS.DATA.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SMS.DATA.Models.SMS>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.SMS context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
