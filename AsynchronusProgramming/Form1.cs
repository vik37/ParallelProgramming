using System.Net;

namespace AsynchronusProgramming
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public async Task<int> CalculateValueAsync()
		{
			await Task.Delay(5000);
			return 123;
		}

		public Task<int> CalculateValueTask()
		{
			return Task.Factory.StartNew(() =>
			{
				Thread.Sleep(5000);
				return 123;
			});
		}

		public int CalculateValue()
		{
			Thread.Sleep(5000);
			return 123;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			#region Blocking Thread
			//DisplayText.Text = "This is something new " + CalculateValue();
			#endregion

			#region Task Contination
			//var calculation = CalculateValueTask();
			//calculation.ContinueWith(t =>
			//{
			//	DisplayText.Text = "Total result is " + t.Result.ToString();
			//}, TaskScheduler.FromCurrentSynchronizationContext());
			#endregion

			#region Async/Await

			DisplayText.Text = "Total result is " + await CalculateValueAsync();

			await Task.Delay(5000);

			//using (var wc = new WebClient()) 
			//{
			//	string data = await wc.DownloadStringTaskAsync("http://google.com/robots.txt");
			//	DisplayText.Text = data.Split("\n")[0].Trim();
			//}

			using (var http = new HttpClient())
			{
				string data = await http.GetStringAsync("http://google.com/robots.txt");

				DisplayText.Text = data.Split("\n")[0].Trim();
			}
			#endregion
		}
	}
}
