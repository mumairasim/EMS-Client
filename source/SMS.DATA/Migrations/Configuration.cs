using System.Data.Entity.Migrations;

namespace SMS.DATA.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Models.SMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.SMSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
