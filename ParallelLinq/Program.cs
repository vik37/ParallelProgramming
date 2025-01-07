namespace ParallelLinq
{
	internal class Program
	{
		#region Custom Aggregation

		static void Main(string[] args)
		{
			//var sum = Enumerable.Range(1,10000).Sum();
			//var sum = Enumerable.Range(1, 1000)
			//	.Aggregate(0, (i, acc) => i + acc);
			var sum = ParallelEnumerable.Range(1, 1000)
				.Aggregate(0,
							(partialSum, i) => partialSum += i,
							(total, subTotal) => total += subTotal,
							i => i);

			Console.WriteLine($"Sum = {sum}");
		}

		#endregion

		#region Merge Options

		//static void Main(string[] args)
		//{
		//	var numbers = Enumerable.Range(1, 80).ToArray();

		//	var results = numbers.AsParallel().WithMergeOptions(ParallelMergeOptions.FullyBuffered).Select(i =>
		//	{
		//		var result = Math.Log10(i);
		//		Console.WriteLine($"Produced {result}\t");
		//		return result;
		//	});

		//	foreach( var result in results)
		//	{
		//		Console.WriteLine($"Consumed {result}\t");
		//	}
		//}

		#endregion

		#region Cancellations & Exceptions

		//static void Main(string[] args)
		//{
		//	var ctx = new CancellationTokenSource();
		//	var items = ParallelEnumerable.Range(0, 20);

		//	var results = items.WithCancellation(ctx.Token).Select(i =>
		//	{
		//		double result = Math.Log10(i);

		//		//if(result > 1) throw new InvalidOperationException();

		//		Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
		//		return result;
		//	});

		//	try
		//	{
		//		foreach(var c in results)
		//		{
		//			if(c > 1) ctx.Cancel();

		//			Console.WriteLine($"result = {c}");
		//		}
		//	}
		//	catch (AggregateException ae)
		//	{
		//		ae.Handle(e =>
		//		{
		//			Console.WriteLine($"{e.GetType().Name} {e.Message}");
		//			return true;
		//		});
		//	}
		//	catch(OperationCanceledException oce)
		//	{
		//		Console.WriteLine("Cancelled");
		//	}
		//}

		#endregion

		#region AsParallel & ParallelQuery

		//static void Main(string[] args)
		//{
		//	const int count = 50;

		//	var items = Enumerable.Range(1, count).ToArray();
		//	var results = new int[count];

		//	items.AsParallel().ForAll(x =>
		//	{
		//		int newValue = x * x * x;
		//		Console.Write($"{newValue} ({Task.CurrentId})\t");
		//		results[x-1] = newValue;
		//	});
		//	Console.WriteLine();
		//	Console.WriteLine();

		//foreach (var i in results)
		//	Console.WriteLine($"{i}\t");
		//Console.WriteLine();

		//var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);

		//foreach(var i in cubes)
		//	Console.Write($"{i}\t");
		//Console.WriteLine();
		//}

		#endregion
	}
}
