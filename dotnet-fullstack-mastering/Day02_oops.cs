using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_fullstack_mastering
{
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
    public class CreditCardPayment : Payment
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
    public sealed class PayPalPayment : Payment
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
