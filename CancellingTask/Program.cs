// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

internal static class Program
{
	public static void Main()
	{
		//var cts = new CancellationTokenSource();
		//var token = cts.Token;

		//token.Register(() => 
		//{
		//	Console.WriteLine("Cancelation has been requested!");
		//});

		//var t = new Task(() =>
		//{
		//	int i = 0;
		//	while (true) 
		//	{
		//		//if(token.IsCancellationRequested)
		//		//	break;
		//		//else

		//		token.ThrowIfCancellationRequested();
		//		Console.WriteLine($"{i++}\t");
		//	}
		//},token);

		//Task.Factory.StartNew(() => 
		//{
		//	token.WaitHandle.WaitOne();
		//	Console.WriteLine($"Wait handle released, cancelation was requested");
		//});

		//t.Start();

		//Console.ReadKey();
		//cts.Cancel();

		#region Compose Cancellation

		var planned = new CancellationTokenSource();
		var preventative = new CancellationTokenSource();
		var emergency = new CancellationTokenSource();

		var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token, preventative.Token, emergency.Token);

		Task.Factory.StartNew(() =>
		{
			int i = 0;
			while(true)
			{
				paranoid.Token.ThrowIfCancellationRequested();
				Console.WriteLine($"{i++}\t");
				Thread.Sleep( 1000 );
			}
		}, paranoid.Token);
		#endregion

		Console.ReadKey();
		emergency.Cancel();

		Console.WriteLine("Main program done");
		Console.ReadKey();
	}
}
