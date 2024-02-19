using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static void Main()
	{
		// ������� ������ ��� ������������
		RunThreadDemo();
		RunAsyncAwaitDemo();

		Console.ReadLine();
	}

	// ������ ����� - ������������ ����� Thread
	static void RunThreadDemo()
	{
		Console.WriteLine("������� ������ � ��������.");

		// ��������� �� ������ ������ ������
		Thread thread = new Thread(ThreadMethod);
		thread.Start();

		// ���������� ���������� ������ ������
		thread.Join();

		Console.WriteLine("���������� ������ � ��������.");
		Console.WriteLine();
	}

	// ����� ��� ��������� � ������
	static void ThreadMethod()
	{
		Console.WriteLine("������� ������ � ������.");
		Thread.Sleep(2000); // ������� ������
		Console.WriteLine("���������� ������ � ������.");
	}

	// ������ ����� - ������������ Async - Await
	static void RunAsyncAwaitDemo()
	{
		Console.WriteLine("������� ������ � Async - Await.");

		// ������ ������������ ������
		Task task = AwaitMethod();
		task.Wait(); // ���������� ���������� ������������ ������

		Console.WriteLine("���������� ������ � Async - Await.");
		Console.WriteLine();
	}

	// ����������� �����
	static async Task AwaitMethod()
	{
		Console.WriteLine("������� ������������ ������.");

		// ���������� ���������� 3 �������
		await Task.Delay(3000);

		Console.WriteLine("���������� ������������ ������.");
	}
}
