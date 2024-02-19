using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static void Main()
	{
		// Виклики методів для демонстрації
		RunThreadDemo();
		RunAsyncAwaitDemo();

		Console.ReadLine();
	}

	// Перший метод - демонстрація класу Thread
	static void RunThreadDemo()
	{
		Console.WriteLine("Початок роботи з потоками.");

		// Створення та запуск нового потоку
		Thread thread = new Thread(ThreadMethod);
		thread.Start();

		// Очікування завершення роботи потоку
		thread.Join();

		Console.WriteLine("Завершення роботи з потоками.");
		Console.WriteLine();
	}

	// Метод для виконання у потоці
	static void ThreadMethod()
	{
		Console.WriteLine("Початок роботи у потоці.");
		Thread.Sleep(2000); // Імітація роботи
		Console.WriteLine("Завершення роботи у потоці.");
	}

	// Другий метод - демонстрація Async - Await
	static void RunAsyncAwaitDemo()
	{
		Console.WriteLine("Початок роботи з Async - Await.");

		// Виклик асинхронного методу
		Task task = AwaitMethod();
		task.Wait(); // Очікування завершення асинхронного методу

		Console.WriteLine("Завершення роботи з Async - Await.");
		Console.WriteLine();
	}

	// Асинхронний метод
	static async Task AwaitMethod()
	{
		Console.WriteLine("Початок асинхронного методу.");

		// Асинхронне очікування 3 секунди
		await Task.Delay(3000);

		Console.WriteLine("Завершення асинхронного методу.");
	}
}
