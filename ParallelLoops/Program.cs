using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace ParallelLoops
{
	public class Program
	{
		#region Partitioning

		[Benchmark]
		public void SquareEachValue()
		{
			const int count = 100000;
			var values = Enumerable.Range(0, count);
			var result = new int[count];
			Parallel.ForEach(values, x => { result[x] = (int)Math.Pow(x, 2); });
		}

		[Benchmark]
		public void SquareEachValueChunk()
		{
			const int count = 100000;
			var values = Enumerable.Range(0, count);
			var result = new int[count];

			var part = Partitioner.Create(0, count, 10000);
			Parallel.ForEach(part, range =>
			{
				for (int i = range.Item1; i < range.Item2; i++) 
				{
					result[i] = (int)Math.Pow(i, 2);
				}
			});
		}

		static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<Program>();
			Console.WriteLine(summary);
		}

		#endregion

		//********************************************\\

		#region Thread Local Storage

		//static void Main(string[] args)
		//{
		//	int sum = 0;

		//	Parallel.For(1, 1001,
		//					() => 0,
		//					(x, state, tls) =>
		//					{
		//						tls += x;
		//						Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
		//						return tls;
		//					},
		//					patrialSum =>
		//					{
		//						Console.WriteLine($"Partioan Value of Task {Task.CurrentId} is {patrialSum}");
		//						Interlocked.Add(ref sum, patrialSum);
		//					});
		//	Console.WriteLine($"Sum of 1 to 100 is {sum}\n");

		//	Console.WriteLine(" --------------------------------------- ");

		//	int simpleSum = 0;

		//	for (int i = 0; i < 1001; i++)
		//	{
		//		simpleSum += i;
		//	}

		//	Console.WriteLine($"Simple sum is {simpleSum}");

		//Parallel.For(1, 1001, x =>
		//{
		//	Interlocked.Add(ref sum, x);
		//});
		//}
		#endregion

		//********************************************\\

		#region Breaking, Cancallations and Exceptions
		//private static ParallelLoopResult _result;
		//public static void Demo()
		//{
		//	var cts = new CancellationTokenSource();

		//	ParallelOptions po = new ParallelOptions();
		//	po.CancellationToken = cts.Token;
		//	_result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
		//	{
		//		Console.WriteLine($"{x}[{Task.CurrentId}]\t");

		//		if (x == 10)
		//		{
		//state.Stop();
		//state.Break();
		//throw new Exception();
		//			cts.Cancel();
		//		}
		//	});

		//	Console.WriteLine($"Was loop completed? {_result.IsCompleted}");
		//	if(_result.LowestBreakIteration.HasValue)
		//		Console.WriteLine($"Lowest break itteration is {_result.LowestBreakIteration.Value}");
		//}

		//static void Main(string[] args)
		//{
		//	try
		//	{
		//		Demo();
		//	}
		//	catch (AggregateException ae)
		//	{
		//		ae.Handle(e =>
		//		{
		//			Console.WriteLine(e.Message);
		//			return true;
		//		});
		//	}
		//	catch(OperationCanceledException oce)
		//	{
		//		Console.WriteLine(oce.Message);
		//	}
		//}
		#endregion

		//********************************************\\

		#region Parallel Invoke/For/ForEach/(Custom ForEarch)
		//public static IEnumerable<int> Range(int start, int end, int step)
		//{
		//	for (int i = start; i < end;i += step)
		//	{
		//		yield return i;
		//	}
		//}

		//static void Main(string[] args)
		//{
		//	var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
		//	var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
		//	var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

		//	Parallel.Invoke( a, b, c );

		//Parallel.For(1,11, i =>
		//{
		//	Console.WriteLine($"{i*i}\t"); 
		//});

		//string[] words = new string[] {"Someones", "gonna", "die", "tonight" };
		//Parallel.ForEach(words, word =>
		//{
		//	Console.WriteLine($"{word} has lenght {word.Length} (Task {Task.CurrentId})");
		//});

		//	Parallel.ForEach(Range(1,20,3),Console.WriteLine);
		//}
		#endregion
	}
}
