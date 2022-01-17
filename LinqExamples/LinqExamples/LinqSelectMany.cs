using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    class LinqSelectMany
    {
        public static void RunSelectMany()
        {

            //all of the list of subjects will flatten to single ienumerable
            var subjects = Students.GetAllStudents().SelectMany(s => s.Subjects);

            subjects.ToList().ForEach(x => Console.WriteLine(x));



            Console.WriteLine("using Query string.");
            //query string

            var subjects_ = from student in Students.GetAllStudents()
                            from subject in student.Subjects
                            select subject;


            subjects_.ToList().ForEach(x => Console.WriteLine(x));




            //using string array

            string[] strarry =
            {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "1234567890"
            };

            strarry.SelectMany(x => x);


            //using query string

            var result = from s in strarry
                         from c in s
                         select c;


            Console.WriteLine("getting distinct.");

            //getting distinct values of subjects
            Students.GetAllStudents().SelectMany(x => x.Subjects).Distinct().ToList().ForEach(c => Console.WriteLine(c));



            Console.WriteLine("distinct with query string.");
            (from student in Students.GetAllStudents()
            from subject in student.Subjects
             select subject).Distinct().ToList().ForEach(x => Console.WriteLine(x));



            Console.WriteLine();
            Console.WriteLine();

            Students.GetAllStudents().SelectMany(s => s.Subjects,
                                                (student, subject) =>
                                                            new { StudentName = student.Name, subjectName = subject })
                .ToList().ForEach(r => Console.WriteLine(r.StudentName + " - " + r.subjectName));



        }
    }


    class Students
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public List<string> Subjects { get; set; }


        public static List<Students> GetAllStudents()
        {
            return new List<Students>
            {
                new Students { Name="Tom", Gender="Male", Subjects =  new List<string> { "asp.net", "c#"} },
                new Students { Name="Mike", Gender="Male", Subjects =  new List<string> { "ado.net", "ajax", "c#"} },
                new Students { Name="Pam", Gender="Female", Subjects =  new List<string> { "WCF", "Sql Server", "c#"} },
                new Students { Name="Mary", Gender="Female", Subjects =  new List<string> { "WPF", "Linq", "Asp.net"} },
            };
        }
    }
}
