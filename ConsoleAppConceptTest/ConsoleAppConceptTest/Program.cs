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
        }

        //check new tuple syntax/format similar to python
        private static (int, string, bool) GetPersonInfo()
        {
            return (30, "John Doe", true);
        }


    }
}
