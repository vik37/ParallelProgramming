namespace TaskCoordination
{
	internal class Program
	{
		#region Manual Reset Event Slim and Auto Reset Event

		static void Main(string[] args)
		{
			//var evt = new ManualResetEventSlim();
			var evt = new ManualResetEvent(false);

			Task.Factory.StartNew(() => 
			{
				Console.WriteLine("Boiling water...");
				evt.Set();
			});

			var makeTea = Task.Factory.StartNew(() => 
			{
				Console.WriteLine("Waiting for the water...");
				//evt.Wait();
				evt.WaitOne();
				Console.WriteLine("Here is your tea.");
				var ok = evt.WaitOne();

				if (ok)
				{
					Console.WriteLine("Enjoy your tea");
				}
				else
				{
					Console.WriteLine("No tea for you");
				}
			});

			makeTea.Wait();
		}

		#endregion

		//********************************************\\

		#region Countdown Event

		//private static int taskCount = 5;
		//static CountdownEvent cte = new CountdownEvent(taskCount);
		//static Random random = new Random();
		//static void Main(string[] args)
		//{
		//	for (int i = 0; i < taskCount; i++)
		//	{
		//		Task.Factory.StartNew(() => 
		//		{
		//			Console.WriteLine($"Entering task {Task.CurrentId}");
		//			Thread.Sleep(random.Next(3000));
		//			cte.Signal();
		//			Console.WriteLine($"Exiting task {Task.CurrentId}");
		//		});
		//	}

		//	var finalTask = Task.Factory.StartNew(() => 
		//	{
		//		Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
		//		cte.Wait();
		//		Console.WriteLine("All tasks completed");
		//	});
		//finalTask.Wait();
		//}

		#endregion

		//********************************************\\

		#region Barrier

		//static Barrier Barrier = new Barrier(2, b =>
		//{
		//	Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
		//});

		//public static void Water()
		//{
		//	Console.WriteLine("Putting the kettle on (takes bit longer)");
		//	Thread.Sleep(2000);
		//	Barrier.SignalAndWait();
		//	Console.WriteLine("Pouring water into cup");
		//	Barrier.SignalAndWait();
		//	Console.WriteLine("Potting the kattle away water");
		//}

		//public static void Cup()
		//{
		//	Console.WriteLine("Finding the nicest cup of tea (fast)");
		//	Barrier.SignalAndWait();
		//	Console.WriteLine("Adding tea.");
		//	Barrier.SignalAndWait();
		//	Console.WriteLine("Adding shugar.");
		//}

		//static void Main(string[] args)
		//{
		//	var water = Task.Factory.StartNew(Water);
		//	var cup = Task.Factory.StartNew(Cup);

		//	var tea = Task.Factory.ContinueWhenAll(new[] {water, cup}, tasks =>
		//	{
		//		Console.WriteLine($"Enjoy your cup of tea.");
		//	});

		//	tea.Wait();
		//}

		#endregion

		//********************************************\\

		#region Task Coordination -> Child Tasks

		//static void Main(string[] args)
		//{
		//	var parent = new Task(() => 
		//	{
		//		// detached
		//		var child = new Task(() => 
		//		{
		//			Console.WriteLine("Child task starting...");
		//			Thread.Sleep(3000);

		//			Console.WriteLine("Child task finishing....");
		//			//throw new Exception();
		//		}, TaskCreationOptions.AttachedToParent);

		//		var completionHandler = child.ContinueWith(t => 
		//		{
		//			Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
		//		}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

		//		var failedHandler = child.ContinueWith(t =>
		//		{
		//			Console.WriteLine($"Ooops, task {t.Id}'s state is {t.Status}");
		//		}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);


		//		child.Start();
		//	});
		//	parent.Start();

		//	try
		//	{
		//		parent.Wait();
		//	}
		//	catch (AggregateException ae)
		//	{
		//		ae.Handle(e => true);
		//	}
		//}

		#endregion

		//********************************************\\

		#region Task Coordination -> Continuations 

		//static void Main(string[] args)
		//{
		//	Console.WriteLine("App start...");

		//	#region Continuations Tasks which don't do anything, but return result

		//	var task = Task.Factory.StartNew(() => "Task 1");
		//	var task2 = Task.Factory.StartNew(() => "Task 2");

		//	//var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 },tasks =>
		//	//{
		//	//	Console.WriteLine("Tasks Completed: ");

		//	//	foreach (var task in tasks)
		//	//		Console.WriteLine(" - " + task.Result);

		//	//	Console.WriteLine("All Tasks are done");
		//	//});

		//	var task3 = Task.Factory.ContinueWhenAny(new[] { task, task2 }, task =>
		//	{
		//		Console.WriteLine("Tasks Completed: ");

		//		Console.WriteLine(" - " + task.Result);

		//		Console.WriteLine("All Tasks are done");
		//	});

		//	#endregion

		//	#region Simple Contination Task Inital Example

		//	//var task = Task.Factory.StartNew(() =>
		//	//{
		//	//	Console.WriteLine("Boiling Water");
		//	//});
		//	//var task2 = task.ContinueWith(t => Console.WriteLine($"Completed task {t.Id}, pour water into cup."));

		//	//Console.WriteLine("App Task 2 wait...");

		//	//task2.Wait();

		//	#endregion

		//	Console.WriteLine("App end...");
		//	Console.ReadKey();
		//}

		#endregion
	}
}
