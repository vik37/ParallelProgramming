namespace Synchronization_CriticalSection
{
	public class BankAccount
	{
		//public object padlock = new object();
		//private int balance;
		public int Balance { get; private set; }

		public void Deposit(int amount)
		{
			//lock (padlock) 
			//{
			//	Balance += amount;
			//}
			//Interlocked.Add(ref balance, amount);
			Balance += amount;
		}
		public void Withdrow(int amount)
		{
			//lock (padlock)
			//{
			//	Balance -= amount;
			//}	
			//Interlocked.Add(ref balance, -amount);
			Balance -= amount;
		}

		public void Transfer(BankAccount where, int amount)
		{
			Balance -= amount;
			where.Balance += amount;
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			var tasks = new List<Task>();
			var ba = new BankAccount();
			var ba2 = new BankAccount();

			Mutex mutex = new Mutex();
			Mutex mutex2 = new Mutex();
			for (int i = 0; i < 10; i++)
			{
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLock = mutex.WaitOne();
						try
						{
							ba.Deposit(1);
						}
						finally
						{
							if (haveLock)
								mutex.ReleaseMutex();
						}
					}
				}));
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLock = mutex2.WaitOne();
						try
						{
							//ba.Withdrow(100);
							ba2.Deposit(1);
						}
						finally
						{
							if (haveLock)
								mutex2.ReleaseMutex();
						}
					}
				}));
				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool haveLocks = WaitHandle.WaitAll(new[] { mutex, mutex2 });
						try
						{
							ba.Transfer(ba2, 1);
						}
						finally
						{
							if (haveLocks)
							{
								mutex.ReleaseMutex();
								mutex2.ReleaseMutex();
							}
						}
					}
				}));
			}

			#region Without Lock
				//for (int i = 0; i < 10; i++)
				//{
				//for (int j = 0; j < 1000; j++)
				//{
				//	ba.Deposit(100);
				//}
				//for (int j = 0; j < 1000; j++)
				//{
				//	ba.Withdrow(100);
				//}
			#endregion

			#region SpinLock
				//SpinLock sl = new SpinLock(true);

				//	tasks.Add(Task.Factory.StartNew(() =>
				//	{
				//		for (int j = 0; j < 1000; j++)
				//		{
				//			bool locktaken = false;
				//			try
				//			{
				//				sl.Enter(ref locktaken);
				//				ba.Deposit(100);
				//			}
				//			finally
				//			{
				//				if (locktaken)
				//					sl.Exit();
				//			}
				//		}
				//	}));
				//	tasks.Add(Task.Factory.StartNew(() =>
				//	{
				//		bool lockTaken = false;
				//		for (int j = 0; j < 1000; j++)
				//		{
				//			try
				//			{
				//				sl.Enter(ref lockTaken);
				//				ba.Withdrow(100);
				//			}
				//			finally
				//			{
				//				if (lockTaken)
				//					sl.Exit();
				//			}
				//		}
				//	}));				
				//}
			#endregion

			Task.WaitAll(tasks.ToArray());
			Console.WriteLine($"Final balance ba is {ba.Balance}");
			Console.WriteLine($"Final balance ba2 is {ba2.Balance}");
		}
	}
}
