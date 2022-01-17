using System;
using System.Collections.Generic;
using System.Linq;


namespace LinqExamples
{
    class LInqAggregate
    {

        public static void runAggregate()
        {
            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            //getting minmum from numbers 
            Console.WriteLine($"Minimum value of array is : {Numbers.Min()}");

            //minimum even value
            Console.WriteLine($"Minmum even values of array is : {Numbers.Where(x => x % 2 == 0).Min()}");

            //sum
            Console.WriteLine($"Max even values of array is : {Numbers.Max()}");



            string[] countries = { "Nepal", "USA", "UK", "Canda", "Australia" };

            //var result = countries.Min(x => x.Length); // count the  minimum length of words in array.

            Console.WriteLine($"coutries with shortest length {countries.Min(x => x.Length)}");
            Console.WriteLine($"coutries with longest length {countries.Max(x => x.Length)}");



            //comma separated.
            Console.WriteLine($"comma separated value is :{countries.Aggregate((a,b) => a + ", " + b)}");


            //cummlative value of numbers 
            Console.WriteLine($"Cummlative value of numbers is: {Numbers.Aggregate((a,b) => a + b)}");

            //using seed with aggregates
            Console.WriteLine($"with seed value {Numbers.Aggregate(10, (a,b) => a * b)}");
        }

    }
}
