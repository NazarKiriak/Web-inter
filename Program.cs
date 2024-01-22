using System;
using System.IO;

class Program
{
	static void Main()
	{
		while (true)
		{
			Console.WriteLine("Choose an option:");
			Console.WriteLine("1. Count the number of words in the text");
			Console.WriteLine("2. Perform a mathematical operation");
			Console.WriteLine("3. Exit");

			Console.Write("Enter the option number: ");
			string input = Console.ReadLine();

			switch (input)
			{
				case "1":
					CountWords();
					break;
				case "2":
					PerformMathOperation();
					break;
				case "3":
					Console.WriteLine("Thank you, bye!");
					return;
				default:
					Console.WriteLine("Please try again.");
					break;
			}

			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}
	}

	static void CountWords()
	{
		string loremText = File.ReadAllText("LoremIpsum.txt");
		int wordCount = loremText.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

		Console.WriteLine($"Number of words in the text: {wordCount}");
	}

	static void PerformMathOperation()
	{
		Console.Write("Enter a mathematical expression to evaluate: ");
		string expression = Console.ReadLine();

		try
		{
			double result = EvaluateMathExpression(expression);
			Console.WriteLine($"Result: {result}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error performing the operation: {ex.Message}");
		}
	}

	static double EvaluateMathExpression(string expression)
	{
		var dataTable = new System.Data.DataTable();
		var result = dataTable.Compute(expression, "");
		return Convert.ToDouble(result);
	}
}
