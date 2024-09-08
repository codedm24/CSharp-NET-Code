using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using static System.Console;

namespace ConsoleAppParallelTask
{
    public class Program2
    {
        static void Main(string[] args)
        {
            //CancelParallelFor();
            CancelTask();
            ReadLine();
        }

        public static void CancelParallelFor()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => { WriteLine("*** token cancelled"); });

            cts.CancelAfter(500);


            try
            {
                ParallelLoopResult result = Parallel.For(0, 100, new ParallelOptions() { CancellationToken = cts.Token },
                    x =>
                    {
                        WriteLine($"loop {x} started");
                        int sum = 0;
                        for (int i = 0; i < 100; i++)
                        {
                            Task.Delay(2).Wait();
                            sum += i;
                            if (cts.IsCancellationRequested)
                                break;
                        }
                        WriteLine($"sum: {sum} for loop {x}");
                        WriteLine($"loop {x} finished");
                    });
            }
            catch (OperationCanceledException ex)
            {
                WriteLine(ex.Message);
            }
        }

        public static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => { WriteLine("*** task cancelled"); });
            cts.CancelAfter(500);

            Task t1 = Task.Run(() =>
            {

                WriteLine("in task");

                for (int i = 0; i <= 100; i++)
                {
                    Task.Delay(100).Wait();
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        //try
                        {
                            WriteLine("cancelling was requested, " +
                                "cancelling from within task");
                            token.ThrowIfCancellationRequested();
                        }
                        //catch (OperationCanceledException ex)
                        {
                            //WriteLine($"Exception: {ex.GetType().Name}, {ex.Message} ");
                        }
                        break;
                    }
                    WriteLine("in loop");
                }
                WriteLine("task finished without cancellation");
            }, cts.Token);
            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                WriteLine($"Exception: {ex.GetType().Name}, {ex.Message}");
                foreach (var innerException in ex.InnerExceptions)
                {
                    WriteLine($"Inner Exception : {ex.InnerException.GetType()}, " +
                        $"{ex.InnerException.Message}");
                }
            }
        }

    }
}
