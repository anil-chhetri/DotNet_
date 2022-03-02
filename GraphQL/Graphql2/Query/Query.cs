using System;
using System.Collections.Generic;
using Bogus;
using Graphql2.Type;

namespace Graphql2.Query
{
    public class Query 
    {

        Faker<InstructorType> instructorFaker;
        Faker<StudentType> StudentFaker;
        Faker<CourseType> CourseFaker;

        public Query()
        {
            instructorFaker = new Faker<InstructorType>()
                .RuleFor(i => i.Id, s => Guid.NewGuid())
                .RuleFor(i => i.FirstName, s => s.Name.FirstName())
                .RuleFor(i => i.LastName, s => s.Name.LastName())
                .RuleFor(i => i.Salary, s => s.Random.Double(10000, 1000000));


           StudentFaker = new Faker<StudentType>()
                .RuleFor(s => s.Id, f => Guid.NewGuid())
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.GPA, f => f.Random.Double(1,4));


             CourseFaker = new Faker<CourseType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.JobTitle())
                .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
                .RuleFor(c => c.Students, f => StudentFaker.Generate(3))
                .RuleFor(c => c.Instructor, f => instructorFaker.Generate());    
        }
        public IEnumerable<CourseType> GetCourses()
        {
            return CourseFaker.Generate(5);    
        }


        public CourseType GetCourseById(Guid id)
        {
            CourseType course  = CourseFaker.Generate();     
            course.Id = id;

            return course;

        }
    }
}