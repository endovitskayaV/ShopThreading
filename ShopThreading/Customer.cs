using System;
using System.Collections.Generic;


namespace ShopThreading
{
   public class Customer
    {
        private static int counter = 1;
        public int id;

        public Customer()
        {
            id = Customer.counter++;
        }

        public void Came()
        {
            Console.WriteLine("Customer " + id + " came to shop.Choosing goods...");
        }

        public void Paid()
        {
            Console.WriteLine("Customer " + id + " payed. Exiting...");
        }

        public void WentAway()
        {
            Console.WriteLine("Customer " + id + " went away.");
        }

        public Cashdesk FindCashdesk(List<Cashdesk> cashdesks)
        {
            foreach (Cashdesk cashdesk in cashdesks)
                if (cashdesk.queue.Count == 0)
                {
                    return cashdesk;
                }
            int min = Int32.MaxValue;
            Cashdesk minCaskdesk=new Cashdesk();
            foreach (Cashdesk cashdesk in cashdesks)
                if (cashdesk.queue.Count<min)
                {
                    minCaskdesk = cashdesk;
                }
            return minCaskdesk;
        }
    }
}
