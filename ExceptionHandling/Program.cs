namespace ExceptionHandling
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var t =Task.Factory.StartNew(() => 
			{ 
				throw new InvalidOperationException("Cant do thiis") { Source = "t"};
			});

			var t2 = Task.Factory.StartNew(() =>
			{
				throw new AccessViolationException("Cant access this") { Source = "t2"};
			});

			try
			{
				Task.WaitAll(t, t2);
			}
			catch (AggregateException ae)
			{ 
				foreach (var e in ae.InnerExceptions)
				{
					Console.WriteLine($"Exception {e.GetType()} from {e.Source}");
				}
			}
			Console.WriteLine("Main program is done.");
			Console.ReadKey();
		}
	}
}
