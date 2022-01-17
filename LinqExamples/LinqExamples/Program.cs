using Microsoft.EntityFrameworkCore.Design;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LinqExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //aggregate
            //LInqAggregate.runAggregate();


            //linq where
            LinqRestrictionsWhere.RunRestrictions(args);


            //Linq select many
            //LinqSelectMany.RunSelectMany();


            //order by then
            LinqOrderBy.RunAll();
        }
    }

    class LinqFactory : IDesignTimeDbContextFactory<LinqDbContext>
    {
        public LinqDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<LinqDbContext>();

            builder
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new LinqDbContext(builder.Options);
        }
    }
}
