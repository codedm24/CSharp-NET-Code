
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

            while (input) {
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

            Task.Run(async ()=>{
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
        }

        //check new tuple syntax/format similar to python
        private static (int, string, bool) GetPersonInfo()
        {
            return (30, "John Doe", true);
        }

        private static void TemporaryGrouping()
        {
            var coordinate = ( x: 5, y: 10);
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

            for (int i = 3; i <= Math.Sqrt(numberToCheck); i++) {
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
           Dictionary<char,int>? wordAndCount = wordToCount?.GroupBy(item => item).ToDictionary(item => item.Key, item => item.Count());
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
    }

    public struct CheckStruct
    {
        public CheckStruct()
        {
        }

        public string StructName { get; set; } = string.Empty;
    }

   
}
