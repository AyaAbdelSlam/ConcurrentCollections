using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingFullExample
{
    class Program
    {
        #region First Module
        //static void Main(string[] args)
        //{
        //	var ordersQueue = new ConcurrentQueue<string>();
        //	Task task1 = Task.Run(() => PlaceOrders(ordersQueue, "Xavier", 5));
        //	Task task2 = Task.Run(() => PlaceOrders(ordersQueue, "Ramdevi", 5));

        //	Task.WaitAll(task1, task2);

        //	foreach (string order in ordersQueue)
        //		Console.WriteLine("ORDER: " + order);
        //}

        //static void PlaceOrders(ConcurrentQueue<string> orders, string customerName, int nOrders)
        //{
        //	for (int i = 1; i <= nOrders; i++)
        //	{
        //		Thread.Sleep(1);
        //		string orderName = $"{customerName} wants t-shirt {i}";
        //		orders.Enqueue(orderName);
        //	}
        //}

        #endregion


        #region Second Module
        static void Main(string[] args)
        {
            // will make an Error because constructor doesnt support init 
            // in concurrent collection

            //var stock = new ConcurrentDictionary<string, int>()
            //{
            //    {"jDays", 4},
            //    {"technologyhour", 3}
            //};
            //IDictionary<string, int> stock = new ConcurrentDictionary<string, int>();

            //// will throws a run time exception if added value already exists in the Dic
            //stock.Add("jDays", 4);
            //stock.Add("technologyhour", 3);

            //Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            //stock.Add("pluralsight", 6);
            //stock["buddhistgeeks"] = 5;

            //stock["pluralsight"] = 7; // up from 6 - we just bought one			
            //Console.WriteLine(string.Format("\r\nstock[pluralsight] = {0}", stock["pluralsight"]));

            //stock.Remove("jDays");

            //Console.WriteLine("\r\nEnumerating:");
            //foreach (var keyValPair in stock)
            //{
            //    Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            //}


            #region TryAdd in Concurrent Collection

            //var stock = new ConcurrentDictionary<string, int>();

            //// will throws a run time exception if added value already exists in the Dic
            //bool success = stock.TryAdd("jDays", 4);
            //Console.WriteLine($"Added Succeeded? {success}");
            //success = stock.TryAdd("pluralsight", 6);
            //success = stock.TryAdd("pluralsight", 6);
            //Console.WriteLine($"Added Succeeded? {success}");

            //Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            //stock.TryAdd("pluralsight", 6);
            //stock["buddhistgeeks"] = 5;

            //stock["pluralsight"] = 7; // up from 6 - we just bought one			
            //Console.WriteLine(string.Format("\r\nstock[pluralsight] = {0}", stock["pluralsight"]));

            //success = stock.TryRemove("jDays", out int jDaysValue);
            //if(success)
            //    Console.WriteLine($"value Removed was: {jDaysValue}");

            //Console.WriteLine("\r\nEnumerating:");
            //foreach (var keyValPair in stock)
            //{
            //    Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            //}

            #endregion

            #region AddOrUpdate

            var stock = new ConcurrentDictionary<string, int>();

            // will throws a run time exception if added value already exists in the Dic
            bool success = stock.TryAdd("jDays", 4);
            Console.WriteLine($"Added Succeeded? {success}");
            success = stock.TryAdd("pluralsight", 6);
            success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine($"Added Succeeded? {success}");

            Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            stock.TryAdd("pluralsight", 6);
            stock["buddhistgeeks"] = 5;

            stock["pluralsight"] = 7; // up from 6 - we just bought one	
            // will fail if the key doenst exist
            Console.WriteLine(string.Format("\r\nstock[pluralsight] = {0}", stock.GetOrAdd("pluralsight", 0)));

            //stock["pluralsight"]++; this is not valid 
            int psStock = stock.AddOrUpdate("pluralsight", 1, (key, oldValue) => oldValue + 1);
            Console.WriteLine($"New Value is {psStock}");
            // Displays whats the new value after the update

            // Displays whats in the dic at a later time
            Console.WriteLine($"New Value is {stock["pluralsight"]}");


            success = stock.TryRemove("jDays", out int jDaysValue);
            if (success)
                Console.WriteLine($"value Removed was: {jDaysValue}");

            Console.WriteLine("\r\nEnumerating:");
            foreach (var keyValPair in stock)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }
            #endregion

            #region Multiple Threads accessing the same collection 

            var task1 = Task.Run(() => stock.TryAdd("jDays01", 4));
            var task2 = Task.Run(() => stock.TryAdd("jDays02", 4));
            Task.Run(() => stock.TryAdd("jDays03", 4));
            //Task task2 = new Task() { };
             Task.WhenAll(task1, task2);
            foreach (var keyValPair in stock)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }


            var dic = new Dictionary<string, int>();


            #endregion

            #region Non-threading examples

            var task01= Task.Run(() => dic.Add("jDays01", 4));
            var task02 = Task.Run(() => dic.Add("jDays02", 4));
            Task.Run(() => dic.Add("jDays03", 4));

            Task.WhenAll(task01, task02);

            foreach (var keyValPair in dic)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }
            #endregion
        }


        #endregion


    }
}