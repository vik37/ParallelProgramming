namespace TaskCoordination
{
	internal class Program
	{
		#region Task Coordination -> Child Tasks

		static void Main(string[] args)
		{
			var parent = new Task(() => 
			{
				// detached
				var child = new Task(() => 
				{
					Console.WriteLine("Child task starting...");
					Thread.Sleep(3000);
					
					Console.WriteLine("Child task finishing....");
					//throw new Exception();
				}, TaskCreationOptions.AttachedToParent);

				var completionHandler = child.ContinueWith(t => 
				{
					Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
				}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

				var failedHandler = child.ContinueWith(t =>
				{
					Console.WriteLine($"Ooops, task {t.Id}'s state is {t.Status}");
				}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);


				child.Start();
			});
			parent.Start();

			try
			{
				parent.Wait();
			}
			catch (AggregateException ae)
			{
				ae.Handle(e => true);
			}
		}

		#endregion

		//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>\\

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
