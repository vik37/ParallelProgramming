using Nito.AsyncEx;

namespace AsynchronusFactoryMethod
{

	#region Asynchronous Lazy Initialization

	public class Stuff
	{
		private static int value;
		private readonly Lazy<Task<int>> _AutoIncValue =
			new Lazy<Task<int>>(async () =>
			{
				await Task.Delay(1000).ConfigureAwait(false);
				return value++;
			});

		private readonly Lazy<Task<int>> _AutoIncValue2 =
			new Lazy<Task<int>>(() => Task.Run(async () =>
			{
				await Task.Delay(1000);
				return value++;
			}));

		// Nito.AsyncEx
		public AsyncLazy<int> AutoIncValue =
			new AsyncLazy<int>(async () =>
			{
				await Task.Delay(1000);
				return value++;
			});

		public async Task UseValue()
		{
			int valueTemp = await _AutoIncValue.Value;
			Console.WriteLine($"Value: {value}");
		}
	}

	internal class Program
	{
		static async Task Main(string[] args)
		{
			var stuff = new Stuff();
			await stuff.UseValue();
		}
	}
	#endregion

	#region Asynchronous Initialization Pattern

	//public interface IAsyncInit
	//{
	//	Task InitTask { get; }
	//}

	//public class MyClass : IAsyncInit
	//{
	//	public Task InitTask { get; }

	//	public MyClass()
	//	{
	//		InitTask = InitAsync();
	//	}

	//	private async Task InitAsync()
	//	{
	//		await Task.Delay(1000);
	//	}
	//}

	//public class MyOtherClass : IAsyncInit
	//{
	//	public Task InitTask { get; }
	//	private readonly MyClass _myClass;

	//	public MyOtherClass(MyClass myClass)
	//	{
	//		this._myClass = myClass;
	//		InitTask = InitAsync();
	//	}

	//	private async Task InitAsync()
	//	{
	//		if (_myClass is IAsyncInit ai)
	//			await ai.InitTask;

	//		await Task.Delay(1000);
	//	}
	//}

	//internal class Program
	//{
	//	static async void Main(string[] args)
	//	{
	//		var myClass = new MyClass();

	//		//if (myClass is IAsyncInit ai)
	//		//	await ai.InitTask;

	//		var oc = new MyOtherClass(myClass);
	//		await oc.InitTask;
	//	}
	//}

	#endregion

	#region Asynchronous Invocation

	//public class Foo
	//{
	//	//public Foo(){}

	//	private Foo(){}

	//	//public async Task<Foo> InitAsync()
	//	//{
	//	//	await Task.Delay(1000);
	//	//	return this;
	//	//}

	//	private async Task<Foo> InitAsync()
	//	{
	//		await Task.Delay(1000);
	//		return this;
	//	}

	//	public static Task<Foo> CreateAsync()
	//	{
	//		var result = new Foo();
	//		return result.InitAsync();
	//	}
	//}

	//internal class Program
	//{
	//	static async Task Main(string[] args)
	//	{
	//		//var foo = new Foo();
	//		//await foo.InitAsync();

	//		Foo x = await Foo.CreateAsync();
	//	}
	//}

	#endregion
}
