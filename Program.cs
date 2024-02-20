using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static void Main()
	{
		//Calling methods for demonstration
		RunThreadDemo();
		RunAsyncAwaitDemo();

		Console.ReadLine();
	}

	//The first method is a demonstration of the Thread class 
	static void RunThreadDemo()
	{
		Console.WriteLine("Getting started with streams");

		//Creating and launching a new thread
		Thread thread = new Thread(ThreadMethod);
		thread.Start();

		//Waiting for the thread to complete 
		thread.Join();

		Console.WriteLine("Completion of work with streams");
		Console.WriteLine();
	}

	//Method to execute in a thread
	static void ThreadMethod()
	{
		Console.WriteLine("Start working in a stream");
		Thread.Sleep(2000);
		Console.WriteLine("Completing work in progress");
	}

	//The second method is the Async - Await demonstration 
	static void RunAsyncAwaitDemo()
	{
		Console.WriteLine("Getting started with Async - Await");

		//Calling an asynchronous method 
		Task task = AwaitMethod();
		task.Wait();

		Console.WriteLine("Completing work with Async - Await");
		Console.WriteLine();
	}

	//Asynchronous method
	static async Task AwaitMethod()
	{
		Console.WriteLine("The beginning of the asynchronous method");

		//Asynchronous waiting 3 seconds
		await Task.Delay(3000);

		Console.WriteLine("Ending an asynchronous method");
	}
}
