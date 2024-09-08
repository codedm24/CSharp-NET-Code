using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleAppParallelTask
{
    public class Program1
    {
        static object s_logLock = new object();

        static void Main(string[] args)
        {
            try
            {
                //ThreadpoolTaskCall();
                //RunSynchronusTask();
                //TaskWithResultDemo();
                //ContinuationTask();
                ParentAndChild();
                ReadLine();
            }
            catch (Exception ex)
            {
                int tt = 0;
            }
        }

        public static void ParentAndChild()
        {
            var parent = new Task(ParentTask);
            parent.Start();
            Task.Delay(2000).Wait();
            WriteLine(parent.Status);
            Task.Delay(4000).Wait(); 
            WriteLine(parent.Status);
        }

        public static void ParentTask()
        {
            WriteLine($"parent task id {Task.CurrentId}");
            var child = new Task(ChildTask);
            child.Start();
            Task.Delay(1000).Wait();
            WriteLine("parent started child");
        }

        public static void ChildTask() { 
            WriteLine($"child task id {Task.CurrentId}");
            Task.Delay(5000).Wait();
            WriteLine("child task finished");
        }

        public static void ContinuationTask() {
            Task t1 = new Task(DoOnFirst);
            Task t2 = t1.ContinueWith(DoOnSecond);
            Task t3 = t1.ContinueWith(DoOnSecond);
            Task t4 = t2.ContinueWith(DoOnSecond);
            t1.Start();
        }

        public static void DoOnFirst() {
            WriteLine($"doing some task {Task.CurrentId}");
            Task.Delay(300).Wait();
        }

        public static void DoOnSecond(Task t)
        {
            WriteLine($"task {t.Id} finished");
            WriteLine($"this task id {Task.CurrentId}");
            WriteLine("do some cleanup");
            Task.Delay(3000).Wait();
        }

        public static void TaskWithResultDemo()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create(8, 3));
            t1.Start();
            WriteLine(t1.Result);
            t1.Wait();
            WriteLine($"result from task: {t1.Result.Item1} {t1.Result.Item2}");
        }

        public static Tuple<int, int> TaskWithResult(object division) {
            Tuple<int, int> div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            WriteLine("task creates a result...");

            return Tuple.Create(result, reminder);
        }

        public static void RunSynchronusTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run sync");
            t1.RunSynchronously();
        }

        public static void ThreadpoolTaskCall()
        {
            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod, "using a task factory");
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");
            var t3 = new Task(TaskMethod, "using a Task constructor and Start");
            t3.Start();
            Task t4 = Task.Run(() => { TaskMethod("using the run method"); });
        }


        public static void TaskMethod(object obj) { 
            Log(obj?.ToString());
        }

        public static void Log(string message)
        {
            lock (s_logLock)
            {
                WriteLine(message);
                WriteLine($"Task Id: {Task.CurrentId?.ToString() ?? "no task"}, " + 
                    $"thread: {Thread.CurrentThread.ManagedThreadId}");

                WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread} ");
                WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
                WriteLine();
            }
        }
    }
}
