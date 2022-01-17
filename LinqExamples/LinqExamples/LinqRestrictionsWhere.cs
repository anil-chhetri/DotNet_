using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    class LinqRestrictionsWhere
    {
        public static void RunRestrictions(string[] args)
        {
            var factory = new LinqFactory();

            using var context = factory.CreateDbContext(args);

            var departments = context.Departments
                                .Include(x => x.Employees)
                                .Where(x => x.Name == "IT" || x.Name == "HR")
                                .OrderBy(x => x.Name).ThenBy(c => c.Location);



            foreach(var dept in departments)
            {
                Console.WriteLine($"Department - {dept.Name}");
                var emp = dept.Employees.Where(x => x.Gender == "Male");
                foreach (var e in emp)
                {
                    Console.WriteLine($"\tEmployee - {e.FirstName}  {e.LastName}");
                }

            }
        }
    }
}
