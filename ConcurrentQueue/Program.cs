using System.Collections.Concurrent;

namespace ConcurrentQueue
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var q = new ConcurrentQueue<int>();
			q.Enqueue(1);
			q.Enqueue(2);

			int result;
			if(q.TryDequeue(out result))
				Console.WriteLine($"Removed element {result}");

			q.Enqueue(3);
			if (q.TryPeek(out result))
				Console.WriteLine($"This element {result} is in the front of the queue");
			Console.ReadKey();
		}
	}
}
