using System.Collections.Concurrent;

namespace Producer_Consumer_Pattern
{
	internal class Program
	{
		static BlockingCollection<int> messages 
			= new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

		static CancellationTokenSource cts = new CancellationTokenSource();

		static Random random = new Random();

		static void ProducerAndConsumer()
		{
			var producer = Task.Factory.StartNew(RunProducer);
			var consumer = Task.Factory.StartNew(RunConsumer);

			try
			{
				Task.WaitAll(new[] { producer, consumer });
			}
			catch (AggregateException ae)
			{
				ae.Handle(e => true);
			}
		}

		static void Main()
		{
			Task.Factory.StartNew(ProducerAndConsumer, cts.Token);

			Console.ReadKey();
			cts.Cancel();
		}

		private static void RunProducer()
		{
			while (true)
			{
				cts.Token.ThrowIfCancellationRequested();
				int i = random.Next(100);
				messages.Add(i);
				Console.WriteLine($"+{i}\t");
				Thread.Sleep(random.Next(1000));
			}
		}

		private static void RunConsumer()
		{
			foreach (var item in messages.GetConsumingEnumerable())
			{
				cts.Token.ThrowIfCancellationRequested();
				Console.WriteLine($"-{item}\t");
				Thread.Sleep(random.Next(1000));
			}
		}
	}
}
