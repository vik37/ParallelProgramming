namespace Task_Programming
{
    internal static class Program
    {
        public static void Write(char c)
        {
            int i = 1000;

            while(i --> 0){
				Console.Write(c);
            }
        }

        public static void Main(string[] args)
        {
            //Task.Factory.StartNew(() => Write('.'));

            //var t = new Task(() => Write('?'));
            //t.Start();
            //Write('_');
            //Console.WriteLine("Hello, World!");

            string text1 = "testing", test2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();
            var task2 = Task.Factory.StartNew<int>(TextLength,test2);
			Console.WriteLine($"Length of {task1} is {task1.Result}");
			Console.WriteLine($"Length of {task2} is {task2.Result}");
            Console.ReadLine();
        }

        public static int TextLength(object o)
        {
			Console.WriteLine($"\nTask with ID: {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }
    }
}
