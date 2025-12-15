//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace dotnet_fullstack_mastering
//{
//    internal class Day01_CSharpBasics
//    {
        

//        static void StringInterpolation()
//        {
//            string fruit = "apple";
//            int quantity = 5;

//            Console.WriteLine($"I have {quantity} {fruit}");
//            Console.WriteLine($"I have {quantity + 1} {fruit}");
//        }

//        static void Formatting()
//        {
//            decimal price = 1234.567m;
//            Console.WriteLine($"Price is: {price:F2}");
//        }

//        static void StringBuilderExample()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append("Hello ");
//            sb.Append("World");

//            Console.WriteLine(sb);
//        }

//        static void NullHandling()
//        {
//            string? name = null;
//            int? age = null;

//            Console.WriteLine($"My age is {(age ?? 21) + 1} and my name is {(name ?? "Rohit")}");

//            Console.WriteLine(name?.Length); // null-safe
//        }
//    }
//}
