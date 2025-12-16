using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFullstackMastering.Basic
{
    // class and object
    internal class Person
    {
        private string Name;
        private int Age;

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public void Introduce()
        {
            Console.WriteLine($"My name is: {Name} and I'm {Age} year old");
        }
    }
    // encapsulation
    internal class BankAccount
    {
        //1.  thread safty: lock object to prevent simultaneous access conflicts
        private readonly object _lock = new object();

        //2. use decimal for money
        //'private set ' ensures only methods inside this class can modify the balance.
        public decimal Balance { get; private set; }
        // this automaticly run a get and set method in background 
        // 1. A hidden private variable (Backing Field)
        /*
        private decimal _balance;

        // 2. The Public Get Method
        public decimal get_Balance()
        {
            return _balance;
        }

        // 3. The Private Set Method
        private void set_Balance(decimal value)
        {
            _balance = value;
        }
        */

        // if we need to write logic inside of our methods 
        /*
         private decimal _balance; // We must bring back the manual field

public decimal Balance
{
    get 
    { 
        Console.WriteLine("Someone is looking at the balance!"); // Extra logic
        return _balance; 
    }
    private set 
    { 
        _balance = value; 
    }
}
         */

        // 3. audit trail: A list of transaction to track history (Encapsulation of logic)
        private readonly List<Transaction> _transactions = new List<Transaction>();

        // Exposing a ReadOnly collection so outside code can view but not modify the list
        public IReadOnlyCollection<Transaction> TransactionHistory => _transactions.AsReadOnly();

        public BankAccount(decimal initialBalance)
        {
            if(initialBalance < 0)
            {
                throw new ArgumentException("Initial balance can not be negative");
            }
            Balance = initialBalance;
            _transactions.Add(new Transaction(initialBalance, "Initial Deposit", DateTime.UtcNow));
        }

        public void Deposit(decimal amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
            }
            // thread locking concurrency control 
            lock (_lock)
            {
                Balance += amount;
                _transactions.Add(new Transaction(amount, "Deposit", DateTime.UtcNow));
            }
        }

        public void Withdraw(decimal amount)
        {
            if(amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");

            lock (_lock)
            {
                if(Balance-amount < 0)
                {
                    throw new InvalidOperationException("Insufficient funds for this withdrawal");
                }
                Balance -= amount;
                _transactions.Add(new Transaction(-amount, "Withdrawal", DateTime.UtcNow));
            }
        }

    }
    // TRANSACTION CLASS
    public class Transaction
    {
        public decimal Amount { get; }
        public string Note { get; }
        public DateTime Date { get; }

        public Transaction(decimal amount, string note, DateTime date)
        {
            Amount = amount;
            Note = note;
            Date = date;
        }
    }

    // inheritance and polymorphism can be added later as needed


    // 1. Inheritance  ABSTRACT BASE CLASS
    // we use 'abstract' because you should never be able to say "new Payment()"
    // It is just a concept not a real thing yet
    public abstract class Payment
    {
        // properties common to all payments
        public string TransactionId { get; }
        public DateTime Timestamp { get; }

        // 'protected' means: "private to the world, but visible to my children"
        protected string _paymentGatewayLogger;

        // constructor for the base class
        protected Payment()
        {
            TransactionId = Guid.NewGuid().ToString().Substring(0, 8);
            Timestamp = DateTime.UtcNow;
            _paymentGatewayLogger = "System_Logs";
        }

        // 2. Abstract Method
        // this force the child to write their own logic: they can not ignore this
        public abstract void Process(decimal amount);


        // 3. VIRTUAL METHOD
        // The child can override this if they want, but they don't have to.
        // we provide a default implementation here
        public virtual void PrintReceipt()
        {
            Console.WriteLine($"[Receipt] Id: {TransactionId} | Time: {Timestamp}");
        }

    }

    // CHILD CLASS 1: Credit card
    public class CreditCardPayment: Payment
    {
        public string CardNumber { get; }

        // 4. CONSTRUCTOR CHAINING (: base)

        public CreditCardPayment(string cardNumber) : base()
        {
            // mask the card number immediately for security
            CardNumber = $"****-****-****-{cardNumber.Substring(cardNumber.Length - 4)}";
        }

        public override void Process(decimal amount)
        {
            Console.WriteLine($"Connecting to Visa Network ({_paymentGatewayLogger})...");
            Console.WriteLine($"Charging {amount:C} to Card {CardNumber}");
            Console.WriteLine($"Transaction Approved.");
        }
    }

    // CHILD CLASS 2: PayPal
    // 5. SEALED CLASS
    // we use 'sealed' to say: "This is the final version. No one can inherit from PaypalPayment"
    public sealed class  PayPalPayment: Payment
    {
        public string Email { get; }
        public PayPalPayment(string email) : base()
        {
                       Email = email;
        }

        public override void Process(decimal amount)
        {
            Console.WriteLine($"Connecting to PayPal Gateway ({_paymentGatewayLogger})...");
            Console.WriteLine($"Charging {amount:C} to PayPal Account {Email}");
            Console.WriteLine($"Payment SuccessFul.");
        }

        public override void PrintReceipt()
        {
            Console.WriteLine($"[PayPal Receipt] Sent to {Email} | ID: {TransactionId}");
        }
    }


}

// INTERFACES

namespace DotNetFullstackMastering.Advanced
{
    // 1. DATA CONTRACT WHAT COMES BACK
    // in real app, we never return 'void' we return a status
    public class PaymentResults
    {
        public bool IsSuccess { get; set; }
        public string TransactionId
        {
            get; set;
        }
        public string Message { get; set; }

    }
    // 2. INTERFACE 
    public interface IPaymentMethods
    {
        bool ValidateDetails();
        PaymentResults ProcessPayment(decimal amount);
    }

    // 3 CONCRETE IMPLEMENTATION; UPI
    public class UpiPayment: IPaymentMethods
    {
        public string UpiId { get; }
        public UpiPayment(string upiId)
        {
            UpiId = upiId;
        }
        public bool ValidateDetails()
        {
            // Real logic: Check regex for UPI ID (e.g., must contain '@')
            return !string.IsNullOrEmpty(UpiId) && UpiId.Contains("@");
        }
        public PaymentResults ProcessPayment(decimal amount)
        {
            if (!ValidateDetails())
                return new PaymentResults
                {
                    IsSuccess = false,
                    TransactionId = null,
                    Message = "Invalid UPI ID"
                };
            Console.WriteLine($"Connecting to UPI Server... Charging {amount:C}");
            return new PaymentResults
            {
                IsSuccess = true,
                TransactionId = "UPI-" + Guid.NewGuid().ToString().Substring(0, 8),
                Message = "Payment Successful via UPI"
            };
        }
    }

    // 4. CONCRETE IMPLEMENTATION: Credit Card
    public class CreditCardPaymentv2 : IPaymentMethods
    {
        public string CardNumber { get; }
        public CreditCardPaymentv2(string cardNumber)
        {
            CardNumber = cardNumber;
        }

        public bool ValidateDetails()
        {
            return CardNumber.Length == 16; // Simplistic check
        }

        public PaymentResults ProcessPayment(decimal amount)
        {
            if (!ValidateDetails())
                return new PaymentResults
                {
                    IsSuccess = false,
                    TransactionId = null,
                    Message = "Invalid Card Number"
                };
            Console.WriteLine($"Connecting to Visa Gateway... Charging {amount:C}");
            return new PaymentResults
            {
                IsSuccess = true,
                TransactionId = "CC-" + Guid.NewGuid().ToString().Substring(0, 8),
                Message = "Credit Card Charged"
            };
        }
    }

    // 5 THE ADVANCED PART: DEPENDENCY INJECTION
    // This class ("The Store") Does NOT KNOW whether you are using UPI or Card.
    // It blindly trusts on the interface

    public class EcommerceStore
    {
        private readonly IPaymentMethods _paymentMethods;
        public EcommerceStore(IPaymentMethods paymentMethods)
        {
            _paymentMethods = paymentMethods;
        }

        public void Checkout(decimal amount)
        {
            Console.WriteLine("--- Starting Cehckout ---");
            if (!_paymentMethods.ValidateDetails())
            {
                Console.WriteLine("Payment details are invalid. Aborting checkout.");
                return;
            }

            var result = _paymentMethods.ProcessPayment(amount);
            if (result.IsSuccess)
            {
                Console.WriteLine($"Payment Successful! Transaction ID: {result.TransactionId}");
            }
            else
            {
                Console.WriteLine($"Payment Failed: {result.Message}");
            }
        }
    }
    // GENERICS CLASS
    public class Storage<T> where T : class
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
            // typeof(T).Name prints "Person" or "Transaction" automatically
            Console.WriteLine($"[Storage] Item added to {typeof(T).Name} list.");
        }

        public T GetAtIndex(int index)
        {
            if (index >= 0 && index < _items.Count)
                return _items[index];
            return null;
        }

        public void PrintCount()
        {
            Console.WriteLine($"Total {_items.Count} items in storage.");
        }
    }

}
