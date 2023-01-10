namespace ReduceApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[3] {1,2,3 };
            int sum = array.Reduce(
                (accumulator, currentValue) => accumulator + currentValue);
            Console.WriteLine(sum); // 6
            
            string[] array2 = new string[3] { "Hell", "o", " World!" };
            string result = array2.Reduce(
                (accumulator, currentValue) => accumulator + currentValue);
            Console.WriteLine(result); //Hello World!

        }
    }
}