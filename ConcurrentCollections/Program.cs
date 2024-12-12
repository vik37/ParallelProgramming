using System.Collections.Concurrent;

namespace ConcurrentCollections
{
	internal class Program
	{
		private static ConcurrentDictionary<string,string> capitals = new ConcurrentDictionary<string,string>();

		public static void AddParis()
		{
			bool success = capitals.TryAdd("France", "Paris");
			string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main Thread";
			Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
		}
		static void Main(string[] args)
		{
			
			Task.Factory.StartNew(AddParis).Wait();
			AddParis();

			//capitals["Russia"] = "Leningrad";
			capitals.AddOrUpdate("Macedonia", "Skopje",(k,v) => v);
			//capitals.AddOrUpdate("Russia", "Moskow", (key, oldValue) => oldValue + "---> Moskow");

			//Console.WriteLine($"The capital of Russia is {capitals["Russia"]}");
			Console.WriteLine($"The capital city in the Republic of Macedonia is {capitals["Macedonia"]}");
			//capitals["Sweden"] = "Uppsala";

			var capitalOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
			Console.WriteLine($"The capital of Sweden is {capitalOfSweden}");

			const string toRemove = "Russia";
			string removed;
			bool didRemove = capitals.TryRemove(toRemove, out removed);

			if (didRemove)
				Console.WriteLine($"We just removed {removed}");
			else
				Console.WriteLine($"Failed to remove the capital of {toRemove}");

			foreach ( var capital in capitals)
			{
				Console.WriteLine($"{capital.Value} is the capital of {capital.Key}");
			}

			Console.ReadKey();
		}
	}
}
