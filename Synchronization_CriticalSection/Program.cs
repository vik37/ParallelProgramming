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
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			var tasks = new List<Task>();
			var ba = new BankAccount();
			SpinLock sl = new SpinLock(true);

			for (int i = 0; i < 10; i++)
			{
				//for (int j = 0; j < 1000; j++)
				//{
				//	ba.Deposit(100);
				//}
				//for (int j = 0; j < 1000; j++)
				//{
				//	ba.Withdrow(100);
				//}

				tasks.Add(Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 1000; j++)
					{
						bool locktaken = false;
						try
						{
							sl.Enter(ref locktaken);
							ba.Deposit(100);
						}
						finally
						{
							if (locktaken)
								sl.Exit();
						}
					}
				}));
				tasks.Add(Task.Factory.StartNew(() =>
				{
					bool lockTaken = false;
					for (int j = 0; j < 1000; j++)
					{
						try
						{
							sl.Enter(ref lockTaken);
							ba.Withdrow(100);
						}
						finally
						{
							if (lockTaken)
								sl.Exit();
						}
					}
				}));				
			}

			Task.WaitAll(tasks.ToArray());
			Console.WriteLine($"Final balance is {ba.Balance}");
		}
	}
}
