using static System.Console;

namespace ConsoleAppParallelTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //ParallelFor();
            ParallelForWithAsync();
            //ParallelForWithStop();
            //ParallelForWithInit();
            //ParallelForEach();
            //ParallelInvoke();
            ReadLine();
        }

        public static void Log(string prefix)
        {
            WriteLine($"{prefix}, task: {Task.CurrentId}, " +
                $"thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void ParallelFor()
        {
            ParallelLoopResult result = Parallel.For(0, 10, i =>
            {
                Log($"S {i}");
                Task.Delay(10).Wait();
                Log($"E {i}");
            });
            WriteLine($"Is Completed: {result.IsCompleted}");
        }

        public static void ParallelForWithAsync()
        {
            ParallelLoopResult result = Parallel.For(0, 10, async i =>
            {
                Log($"S {i}");
                await Task.Delay(10);
                Log($"E {i}");
            });

            WriteLine($"Is Completed: {result.IsCompleted}");
        }

        public static void ParallelForWithStop()
        {
            ParallelLoopResult result = Parallel.For(10, 40, (int i, ParallelLoopState pls) =>
            {
                Log($"S {i}");
                if (i > 12)
                {
                    pls.Break();
                    Log($"break now...{i}");
                }
                Task.Delay(10).Wait();
                Log($"E {i}");
            });

            WriteLine($"Is Completed: {result.IsCompleted}");
            WriteLine($"lowest break iteration: {result.LowestBreakIteration}");
        }

        public static void ParallelForWithInit()
        {
            Parallel.For<string>(0, 10, () =>
            {
                Log($"init thread");
                return $"t{Thread.CurrentThread.ManagedThreadId}";
            },
            (i, pls, str1) =>
            {
                Log($"body i {i} str1 {str1}");
                Task.Delay(10).Wait();
                return $"i {i}";
            },
            (str1) =>
            {
                Log($"finally {str1}");
            });
        }

        public static void ParallelForEach()
        {
            string[] data = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };

            ParallelLoopResult result = Parallel.ForEach<string>(data, s => { WriteLine(s); });
        }

        public static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }

        public static void Foo()
        {
            WriteLine("foo");
        }

        public static void Bar()
        {
            WriteLine("bar");
        }
    }
}
