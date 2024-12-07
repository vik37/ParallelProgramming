namespace ReaderWriterLock
{
	internal class Program
	{
		static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
		static Random rand = new Random();

		static void Main(string[] args)
		{
			int x = 0;
			var tasks = new List<Task>();
			for (int i = 0; i < 10; i++)
			{
				tasks.Add(Task.Factory.StartNew(() =>
				{
					//padLock.EnterReadLock();
					//padLock.EnterReadLock();

					padLock.EnterUpgradeableReadLock();

					if (i % 2 == 0)
					{
						padLock.EnterWriteLock();
						x = 123;
						padLock.ExitWriteLock();
					}

					Console.WriteLine($"Enter read lock x = {x}");
					Thread.Sleep(5000);

					//padLock.ExitReadLock();
					//padLock.ExitReadLock();

					padLock.ExitUpgradeableReadLock();

					Console.WriteLine($"Exited read lock x = {x}");
				}));
			}

			try
			{
				Task.WaitAll(tasks.ToArray());
			}
			catch (AggregateException ae)
			{
				ae.Handle(e =>
				{
					Console.WriteLine(e);
					return true;
				});
			}

			while (true)
			{
				Console.ReadKey();
				padLock.EnterWriteLock();
				Console.WriteLine("Write lock required!");
				int newValue = rand.Next();
				x = newValue;
				Console.WriteLine($"Set x = {x}");
				padLock.ExitWriteLock();
				Console.WriteLine("Write lock released!!!");
			}
		}
	}
}
