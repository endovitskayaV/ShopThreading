using System;
using System.Collections.Generic;
using System.Threading;

namespace ShopThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cashdesk> cashdesks = new List<Cashdesk>();
            Console.WriteLine("Enter number of customers");
            Console.Write("number=");
            int customersNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number of cashdesks");
            Console.Write("number=");
            int cashdesksNumber = Convert.ToInt32(Console.ReadLine());
            Random rnd = new Random();

            for (int i = 0; i < cashdesksNumber; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Cashdesk cashdesk = new Cashdesk();
                    cashdesks.Add(cashdesk);
                    cashdesk.Idle();
                });
                thread.Start();
            }

            for (int i = 0; i < customersNumber; i++)
            {
                Thread threadCustomer = new Thread(() =>
                 {
                     int randsec = rnd.Next(0, 5000);
                     Thread.Sleep(5000 + randsec);
                     Customer customer = new Customer();
                     customer.Came();
                     randsec = rnd.Next(0, 5000);
                     Thread.Sleep(1000 + randsec);
                     Cashdesk cashdesk = customer.FindCashdesk(cashdesks);
                     cashdesk.Enqueue(customer).WaitOne();
                     customer.Paid();
                     customer.WentAway();
                 });
               threadCustomer.Start();
            }
            Console.ReadKey();

        }
    }
}
