namespace ConsoleAppConceptTest
{
    internal class Program
    {
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
