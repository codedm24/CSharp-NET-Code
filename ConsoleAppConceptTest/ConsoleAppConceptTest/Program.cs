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


             
    }
}
