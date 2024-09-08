using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static System.Console;

namespace ConsoleAppParallelTask
{
    public class Program3
    {
        private static BufferBlock<string> s_buffer = new BufferBlock<string>();

        static void Main(string[] args)
        {
            Task t1 = Task.Run(() => { Producer(); });
            Task t2 = Task.Run(async () => { await ConsumerAsync(); });
            Task.WaitAll(t1, t2);
            ReadLine();
        }
        
        public static void Producer()
        { 
            bool exit = false;
            while(!exit)
            {
                string? input = ReadLine();
               
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else { 
                   s_buffer.Post(input);
                }
            }
        }

        public static async Task ConsumerAsync()
        { 
            while(true)
            {
                string data = await s_buffer.ReceiveAsync();
                WriteLine($"user input: {data}");
            }
        }
    }
}
