using System;
using System.Collections.Generic;
using System.Threading;

namespace ShopThreading
{
    public class Cashdesk
    {
        private static int counter = 1;
        public int id;
        public Random random;
        public Queue<Tuple<Customer, EventWaitHandle>> queue;

        public Cashdesk()
        {
            id = Cashdesk.counter++;
            queue = new Queue<Tuple<Customer, EventWaitHandle>>();
            random = new Random();
        }

        public void Idle()
        {
            while (queue.Count == 0);
            Paying();
        }

        public EventWaitHandle Enqueue(Customer customer)
        {
            Console.WriteLine("Customer " + customer.id + " came to cashdesk " + id + ". ");
            if (queue.Count > 0)
            {
                Console.Write(queue.Count + " customers in queue:" );
                foreach (Tuple<Customer, EventWaitHandle> tuple in queue)
                    Console.Write(tuple.Item1.id+ ", ");
                Console.WriteLine(" Waiting...");
            }
            EventWaitHandle ewh = new AutoResetEvent(false);
            queue.Enqueue(Tuple.Create(customer, ewh));
            return ewh;
    }

        public void Paying()
        {
            while (queue.Count > 0)
            {
                Tuple<Customer, EventWaitHandle> tuple = queue.Dequeue();
                Console.WriteLine("Customer " + tuple.Item1.id  + " paying at " + id + "...");
                int randsec = random.Next(0, 5000);
                Thread.Sleep(3000); 
                tuple.Item2.Set();
            }
            Idle();
        }  
    }
}
