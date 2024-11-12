
using System;
using static System.Console;

namespace ConsoleAppConceptTest
{
    internal class Program
    {
        internal class Person
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var personInfo = GetPersonInfo();
            var age = personInfo.Item1;
            var name = personInfo.Item2;
            var isEmployed = personInfo.Item3;
            Console.WriteLine($"Age: {age}, Name: {name}, IsEmployed:{isEmployed}");
            TemporaryGrouping();
            ShortLivedDataStructure();
            Person newPerson = new();
            Console.WriteLine($"Person FirstName:{newPerson.FirstName}");
            Person newPerson1 = new Person { FirstName = "John", LastName = "Doie", Address = "Kolkata" };
            Console.WriteLine($"Person FirstName:{newPerson1.FirstName}, LastName:{newPerson1.LastName}");
            Console.WriteLine($"Calling {nameof(ReverseAStringUsingArray)}");
            ReverseAStringUsingArray();
            Console.WriteLine($"Calling {nameof(ReverseAStringUsingLinQ)}");
            ReverseAStringUsingLinQ();

            bool input = true;

            while (input)
            {
                WriteLine("Enter word to get character count");
                string? inputWord = ReadLine();
                if (inputWord?.ToLower() == "exit")
                    break;
                WriteLine($"letter and count for word: {inputWord}");
                GetCharacterCount(inputWord);
            }

            input = true;

            while (input)
            {
                Console.WriteLine("Enter word to check palindrome");
                string? inputWord = ReadLine();
                if (inputWord?.ToLower() == "exit")
                    break;
                CheckForPalindrome(inputWord);
            }

            Task.Run(async () =>
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                Task task = LongRunningOperationAsync(cts.Token);

                // Simulate some other work
                await Task.Delay(5000);

                // Cancel the operation
                cts.Cancel();

                try
                {
                    await task;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation was canceled.");
                }
            }).Wait();

            CheckStruct checkStruct = new CheckStruct();
            checkStruct.StructName = nameof(checkStruct);
            WriteLine($"Struct name: {checkStruct.StructName}");

            ShowTimeDifference();

            //instantiate object class
            //var obj = new object();

            //infinite loop
            //for (; ; )
            //{
            //    WriteLine("Infinite loop");
            //    Task.Delay(100);
            //}

            for (int i = 0; i < 10; i++)
            {
                //executin type 1
                //Task.Run(async () => {
                //    await RunALoopReturnsTask(i);
                //}).Wait();
                //execution type 2
                //Task.Run(() => {
                //  RunALoopReturnsVoidWithAwait(i);
                //}).Wait();
                //execution type 3
                RunALoopReturnsVoidWithWait(i);
            }

            CheckArrayCopyClone();

            CheckAbstractClassInheritance();

            //checking multicast delegate
            Subscriber1 subscriber1 = new Subscriber1();
            Subscriber2 subscriber2 = new Subscriber2();

            ProcessBusinessLogic processBusinessLogic = new ProcessBusinessLogic();
            processBusinessLogic.ProcessCompleted += subscriber1.NotifyMe;
            processBusinessLogic.ProcessCompleted += subscriber2.NotifyMe;

            processBusinessLogic.StartProcess();

            processBusinessLogic.ProcessCompleted -= subscriber1.NotifyMe;

            processBusinessLogic.StartProcess();

            CheckQueueImplementation();
        }

        //check new tuple syntax/format similar to python
        private static (int, string, bool) GetPersonInfo()
        {
            return (30, "John Doe", true);
        }

        private static void TemporaryGrouping()
        {
            var coordinate = (x: 5, y: 10);
            Console.WriteLine($"X: {coordinate.x}, Y: {coordinate.y}");

        }

        private static void ShortLivedDataStructure()
        {
            var book = ("1984", "George Orwell", 1949);
            Console.WriteLine($"Title: {book.Item1}, Author: {book.Item2}, Year: {book.Item3}");
        }

        private static void ReverseAStringUsingLinQ()
        {
            string? originalstring = "Hello World";
            Console.WriteLine($"Original string: {originalstring}");
            string? reverseString = new string(originalstring.Reverse().ToArray<Char>());
            Console.WriteLine($"Reverse string: {reverseString}");
        }

        private static void ReverseAStringUsingArray()
        {
            string? originalstring = "Hello World";
            Console.WriteLine($"Original string: {originalstring}");
            char[] charArray = originalstring.ToCharArray();
            Array.Reverse(charArray);
            string? reverseString = new string(charArray);
            Console.WriteLine($"Reverse string: {reverseString}");
        }

        private static void CheckForPalindrome(string? wordToCheck)
        {
            string cleanedWord = new string(wordToCheck?.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            char[] charArray = cleanedWord.ToCharArray();
            Array.Reverse(charArray);
            string reversedWord = new string(charArray);
            if (cleanedWord == reversedWord)
                WriteLine($"Word {wordToCheck} is a palindrome");
            else
                WriteLine($"Word {wordToCheck} is not a plaindrome");
        }

        private static void CheckForAPrime(int numberToCheck)
        {
            bool isPrime = true;
            if (numberToCheck <= 1)
                isPrime = false;
            if (numberToCheck == 2)
                isPrime = true;
            if (numberToCheck % 2 == 0)
                isPrime = false;

            for (int i = 3; i <= Math.Sqrt(numberToCheck); i++)
            {
                if (i % 3 == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            WriteLine($"Given number {numberToCheck} is {(isPrime ? "a Prime" : "not a Prime")}");
        }

        private static void GetCharacterCount(string? wordToCount)
        {
            Dictionary<char, int>? wordAndCount = wordToCount?.GroupBy(item => item).ToDictionary(item => item.Key, item => item.Count());
            if (wordAndCount != null)
            {
                foreach (char letter in wordAndCount.Keys)

                    WriteLine($"Letter: {letter}, Count: {wordAndCount[letter]}");
            }
        }

        private static async Task LongRunningOperationAsync(CancellationToken token)
        {
            for (int i = 0; i < 100; i++)
            {
                token.ThrowIfCancellationRequested();
                // Simulate work
                //WriteLine($"Count: {i}");
                //await Task.Delay(100);
                await TaskForLoop(i);
            }

            WriteLine("End of LongRunningOperationAsync");
        }

        private static async Task TaskForLoop(int i)
        {
            WriteNumber(i);
            await Task.Delay(100);
        }

        private static void WriteNumber(int i)
        {
            WriteLine($"value: {i}");
        }

        private static void ShowTimeDifference()
        {
            string startTime = "2023-12-06 23:00:00";
            string endTime = "2023-12-07 01:00:00";

            DateTime start = DateTime.ParseExact(startTime, "yyyy-MM-dd HH:mm:ss", null);
            DateTime end = DateTime.ParseExact(endTime, "yyyy-MM-dd HH:mm:ss", null);

            TimeSpan rideDuration = end - start;
            Console.WriteLine("Duration: " + rideDuration);
        }

        private static async Task RunALoopReturnsTask(int index)
        {
            for (int i = 0; i < 10; i++)
            {
                WriteLine($"Index: {index}, Count: {i}");
                await Task.Delay(100);
            }
        }

        private static async void RunALoopReturnsVoidWithAwait(int index)
        {
            for (int i = 0; i < 10; i++)
            {
                WriteLine($"Index: {index}, Count: {i}");
                await Task.Delay(100);
            }
        }

        private static async void RunALoopReturnsVoidWithWait(int index)
        {
            for (int i = 0; i < 10; i++)
            {
                WriteLine($"Index: {index}, Count: {i}");
                Task.Delay(100).Wait();
            }
        }

        /// <summary>
        /// Shallowcopy
        /// </summary>
        private static void CheckArrayCopyClone()
        {
            int[] arr1 = new int[] { 1, 2, 3, 4, 5 };

            WriteLine($"arr1[0]: " + arr1[0]);

            int[] arr2 = new int[5];
            arr1.CopyTo(arr2,0);

            arr2[0] = 6;

            int[] arr3 = (int[])arr1.Clone();

            arr3[0] = 7;

            WriteLine($"arr1[0]: " + arr1[0]);
            WriteLine($"arr2[0]: " + arr2[0]);
            WriteLine($"arr3[0]: " + arr3[0]);

            if (arr1[0].Equals(arr2[0]))
                WriteLine("same object");
            else
                WriteLine("Not same object");

            Person person1 = new Person { FirstName = "John", LastName = "Doe", Address = "ABC" };
            Person person2 = new Person { FirstName = "Mike", LastName = "Run", Address = "DEF" };
            Person person3 = new Person { FirstName = "Oscar", LastName = "Hold", Address = "GHI" };

            Person[] personArr1 = new Person[] { person1, person2, person3 };
            Person[] personArr2 = new Person[3];

            personArr1.CopyTo(personArr2, 0);

            Person[] personArr3 = (Person[])personArr1.Clone();

            personArr2[0].FirstName = "Roy";

            personArr3[0].FirstName = "James";

            WriteLine($"personArr1[0] FirstName:" + personArr1[0].FirstName);
            WriteLine($"personArr2[0] FirstName:" + personArr2[0].FirstName);
            WriteLine($"personArr3[0] FirstName:" + personArr3[0].FirstName);
            return;
        }

        private static void CheckAbstractClassInheritance()
        { 
            CustomShape shape1 = new CustomShape3DCuboid();
            shape1.SetX(5);
            shape1.SetY(5);
            shape1.SetZ(5);
            double area = shape1.Area;
        }

        private static void CheckQueueImplementation()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            WriteLine("Items in queue");
            PrintQueue(queue);

            int dequeuedElement = queue.Dequeue();
            WriteLine($"\nDequeued element: {dequeuedElement}");

            WriteLine("queue after dequeueing one element");
            PrintQueue(queue);

            int fronElement = queue.Peek();
            WriteLine($"\nFront element: {fronElement}");

            bool containsTwo = queue.Contains(2);
            WriteLine($"\nqueue contains 2: {containsTwo}");

            int count = queue.Count;
            WriteLine($"\nNumber of elements in the queue: {count}");
        }

        private static void PrintQueue(Queue<int> queue)
        {
            foreach (var item in queue)
                Write(item + " ");
        }
    }

    public struct CheckStruct
    {
        public CheckStruct()
        {
        }

        public string StructName { get; set; } = string.Empty;
    }

    public abstract class Shape
    {
        public Shape()
        {
            WriteLine("Constructor of Shape base class called");
        }
        public abstract void SetX(int x);
        public abstract void SetY(int y);
        public virtual double Area { get; }
    }

    public abstract class CustomShape : Shape
    {
        public CustomShape()
        {
            WriteLine("Constructor of CustomShape base class called");
        }

        public abstract void SetZ(int z);
    }

    public class CustomShape3DCuboid : CustomShape
    {
        private int m_X = 0, m_Y = 0, m_Z = 0;

        public CustomShape3DCuboid()
        {
            WriteLine("Constructor of CustomShape3D called");
        }

        public override void SetX(int x)
        {
            m_X = x;
        }

        public override void SetY(int y)
        {
            m_Y = y;
        }

        public override void SetZ(int z)
        {
            m_Z = z;
        }

        public override double Area
        {
            get {
                double area = 2*(m_X * m_Y) + 2*(m_Y * m_Z) + 2 * (m_X * m_Z);
                WriteLine($"Area is {area}");
                return area;
            }
        }

    }

    public delegate void Notify(string message);

    public class ProcessBusinessLogic
    {
        public event Notify ProcessCompleted;

        public void StartProcess()
        {
            WriteLine("Process started...");

            OnProcessCompleted("Process completed successfully.");
        }

        protected virtual void OnProcessCompleted(string message)
        {
            ProcessCompleted?.Invoke(message);
        }
    }

    public class Subscriber1
    {
        public void NotifyMe(string message)
        {
            WriteLine("Subscriber1 received message: " + message);
        }
    }

    public class Subscriber2
    {
        public void NotifyMe(string message)
        {
            WriteLine("Subscriber2 received message: " + message);
        }
    }


}
