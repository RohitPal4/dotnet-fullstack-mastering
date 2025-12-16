using DotNetFullstackMastering.Basic;
using DotNetFullstackMastering.Advanced;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dotnet_fullstack_mastering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 👇 ADD THIS LINE TO FIX THE '?' SYMBOL  its for currency symbols like ₹ $ €
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            /*
            int x, y, z;




            int age = 23;

            Console.WriteLine(age);
            Console.WriteLine(int.MaxValue);
            Console.WriteLine(int.MinValue);

            long bigNumber = 12345678L;
            Console.WriteLine(bigNumber);
            Console.WriteLine(long.MaxValue);
            Console.WriteLine(long.MinValue);

            double negative = -12.34D;
            Console.WriteLine(double.MaxValue);
            Console.WriteLine(double.MinValue);

            float precision = 12.34F;
            Console.WriteLine(precision);
            Console.WriteLine(float.MaxValue);
            Console.WriteLine(float.MinValue);

            decimal money = 123.99M;
            Console.WriteLine(money);
            Console.WriteLine(decimal.MaxValue);
            Console.WriteLine(decimal.MinValue);
            */

            // strings and characters
            /*
                        string firstName = "John";
                        char lastName = 'd';
                        string name = "";
                        char letter = '\0';
            */

            //Console.Write(firstName);
            //Console.WriteLine();
            //Console.Write(lastName);

            // converting text to numbers
            /*string numberText = "1234";
            int number = int.Parse(numberText);
            int number2 = Convert.ToInt32(numberText);
            Console.WriteLine(number);
            Console.WriteLine(number2);
            Console.WriteLine(); */

            // convert.ToString() convert.ToDouble() convert.ToDecimal() convert.ToLong() convert.ToFloat() convert.ToSingle()
            /* // STRING
             // Convert.ToString(value)        -> Converts any type to string (null safe)
             // value.ToString()               -> Converts to string (throws error if value is null)

             // INTEGER (32-bit)
             // Convert.ToInt32(value)         -> Converts to int (handles null)
             // int.Parse(value)               -> Converts string to int (throws exception)
             // int.TryParse(value, out x)     -> Safe conversion, no exception

             // LONG (64-bit)
             // Convert.ToInt64(value)         -> Converts to long
             // long.Parse(value)              -> Converts string to long
             // long.TryParse(value, out x)    -> Safe conversion

             // DOUBLE
             // Convert.ToDouble(value)        -> Converts to double
             // double.Parse(value)            -> Converts string to double
             // double.TryParse(value, out x)  -> Safe conversion

             // DECIMAL (Financial)
             // Convert.ToDecimal(value)       -> Converts to decimal (high precision)
             // decimal.Parse(value)           -> Converts string to decimal
             // decimal.TryParse(value, out x) -> Safe conversion

             // FLOAT (Single Precision)
             // Convert.ToSingle(value)        -> Converts to float
             // float.Parse(value)             -> Converts string to float
             // float.TryParse(value, out x)   -> Safe conversion

             // BOOLEAN
             // Convert.ToBoolean(value)       -> Converts to bool (true/false, 1/0)
             // bool.Parse(value)              -> Converts string to bool
             // bool.TryParse(value, out x)    -> Safe conversion

             // CHARACTER
             // Convert.ToChar(value)          -> Converts to char (single character only)
             // char.Parse(value)              -> Converts string to char
             // char.TryParse(value, out x)    -> Safe conversion

             // DATETIME
             // Convert.ToDateTime(value)      -> Converts to DateTime (culture-based)
             // DateTime.Parse(value)          -> Converts string to DateTime
             // DateTime.TryParse(value, out x)-> Safe conversion

             // BYTE
             // Convert.ToByte(value)          -> Converts to byte (0–255)
             // byte.Parse(value)              -> Converts string to byte
             // byte.TryParse(value, out x)    -> Safe conversion

             // SIGNED BYTE
             // Convert.ToSByte(value)         -> Converts to sbyte (-128 to 127)
             // sbyte.Parse(value)             -> Converts string to sbyte
             // sbyte.TryParse(value, out x)   -> Safe conversion

             // UNSIGNED SHORT
             // Convert.ToUInt16(value)        -> Converts to ushort
             // ushort.Parse(value)            -> Converts string to ushort
             // ushort.TryParse(value, out x)  -> Safe conversion

             // UNSIGNED INT
             // Convert.ToUInt32(value)        -> Converts to uint
             // uint.Parse(value)              -> Converts string to uint
             // uint.TryParse(value, out x)    -> Safe conversion

             // UNSIGNED LONG
             // Convert.ToUInt64(value)        -> Converts to ulong
             // ulong.Parse(value)             -> Converts string to ulong
             // ulong.TryParse(value, out x)   -> Safe conversion
 */

            // atring interpolation
            string fruit = "apple";
            int quantity = 5;
            Console.WriteLine($"i have {quantity} {fruit}");
            Console.WriteLine($"i have {quantity+1} {fruit}");

            //Formatting
            decimal price = 1234.567m;
            Console.WriteLine($"Price is: {price:F2}");

            //string is immutable so we cant change value directly
            //string s = "Hello";
            //s += " World"; its not allowed in c#

            //correct way 
            StringBuilder sb = new StringBuilder();
            sb.Append("Hello ");
            sb.Append("World");
            Console.WriteLine(sb);

            // null in c# 
            /*string name = null;
            Console.WriteLine(name.Length); // ❌ NullReferenceException*/

            //Nullable Reference Types
            /*string? name = null; */

            /* string? name = null;
             string finalName = name ?? "Guest";
             Console.WriteLine(name);*/

            //7.Null - Conditional Operator(?.)(ADVANCED)
            /*string? name = null;
            Console.WriteLine(name?.Length); */

            //use of object and dynamic
            /* object x = "Hello";
             //Console.WriteLine(x.Length);
             dynamic y = "Hello";
             Console.WriteLine(y.Length); not use dynamic unless its nessasry
            */

            /* string? name = null;
             int?age = null;
             Console.WriteLine($"my age is : {(age??21)+1} and my name is {(name??"Rohit")}");
            */
            // class and object

            /*Person person = new Person("Rohit", 21);
            person.Introduce();

            */
            // encapsulation
            /*
            BankAccount account = new BankAccount(1000m);
            account.Deposit(500m);
            Console.WriteLine(account.Balance);
            account.Withdraw(200m);
            Console.WriteLine(account.Balance);

            Console.WriteLine($"Current Balance: {account.Balance:C}");
            Console.WriteLine("\n--- TRANSACTION HISTORY ---");

            foreach(var transaction in account.TransactionHistory)
            {
                Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Note}: {transaction.Amount:C}");
            }
            */

            // inheritance
            // 6. polymorphism
            // we can treat different objects as the same 'Base' type.
            /*List<Payment> pendingPayments = new List<Payment>
            {
                new CreditCardPayment("1234567890123456"),
                new PayPalPayment("rohit@example.com"),
                new CreditCardPayment("9876543210987654")
            };

            Console.WriteLine("--- BATCH PROCESSING START ---\n");
            foreach(var payment in pendingPayments)
            {
                payment.Process(100.00m);
                payment.PrintReceipt();
                Console.WriteLine();
            }
            */
            
            // INTERFACE
            
            // Scenario 1: Customer wants to pay via UPI
            // We create the specific payment object...
            IPaymentMethods myUpi = new UpiPayment("rohit@upi");
            // ... and inject into the store
            EcommerceStore amazon = new EcommerceStore(myUpi);
            amazon.Checkout(500.00m);

            Console.WriteLine("\n-------------------\n");

            // Scenario 2: Customer changes mind, uses Card
            // notice we create a NEW store instance with DIFFERENT behavior
            IPaymentMethods myCard = new CreditCardPaymentv2("1234123412341234");

            EcommerceStore flipKart = new EcommerceStore(myCard);

            flipKart.Checkout(1200.00m);



            // ==========================================
            // 👇 7. GENERICS (ADD THIS NEW CODE BELOW) 👇
            // ==========================================
            Console.WriteLine("\n--- 7. GENERICS (Storage<T>) ---");

            // 1. Create storage for Persons
            // We tell the Storage class: "T is now a Person"
            Storage<Person> personStorage = new Storage<Person>();

            // .Add() now expects a Person object
            personStorage.Add(new Person("Rohit", 23));
            personStorage.Add(new Person("Admin", 30));
            personStorage.PrintCount();

            // 2. Create storage for Transactions (USING THE SAME CLASS!)
            // We tell the Storage class: "T is now a Transaction"
            Storage<Transaction> transactionStorage = new Storage<Transaction>();

            // .Add() now expects a Transaction object
            transactionStorage.Add(new Transaction(500, "Demo 1", DateTime.Now));
            transactionStorage.Add(new Transaction(100, "Demo 2", DateTime.Now));
            transactionStorage.PrintCount();

            Console.ReadLine();


        }
    }
}
