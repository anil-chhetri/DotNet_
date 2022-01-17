using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    class LinqOrderBy
    {
        public static void RunAll()
        {
            //not working-> 1st order by totalmarks and then the resultant will be order by name.
            //Student.GetAllStudents().OrderBy(x => x.TotalMarks).OrderBy(x => x.Name).ToList()
            //    .ForEach(x => Console.WriteLine(x.TotalMarks + "\t" + x.Name + "\t" + x.StudentId ));


            Student.GetAllStudents().OrderBy(x => x.TotalMarks).ThenBy(s => s.Name).ThenBy(i => i.StudentId)
                .ToList().ForEach(x => Console.WriteLine(x.TotalMarks + "\t" + x.Name + "\t" + x.StudentId));


            
        }
    }


    class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public int TotalMarks { get; set; }


        public static List<Student> GetAllStudents()
        {
            return new List<Student>()
            {
                new Student { Name="Tom", StudentId=1, TotalMarks=800},
                new Student { Name="Mary", StudentId=2, TotalMarks=900},
                new Student { Name="Pam", StudentId=3, TotalMarks=800},
                new Student { Name="John", StudentId=4, TotalMarks=800},
                new Student { Name="John", StudentId=4, TotalMarks=800}
            };
        }
    }
}
