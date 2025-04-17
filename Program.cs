using System;
using System.Collections.Generic;
public sealed class ConsoleCalculator
{
	private string _greeting;
	private List<string> history = new();
	private static Dictionary<string, Dictionary<string, string>>translations = new()
	{
 {
 "en", new Dictionary<string, string>
 {
 { "greeting", "Welcome to the calculator!" },
 { "chooseOperator", "Choose an operator:" },
 { "invalidChoice", "Invalid choice. Please try again." },
 { "result", "The result is: " },
 { "exit", "Goodbye!" }
 }
 },
 {
 "ru", new Dictionary<string, string>
 {
 { "greeting", "Добро пожаловать в калькулятор!" },
 { "chooseOperator", "Выберите оператор:" },
 { "invalidChoice", "Неверный выбор. Попробуйте снова." },
 { "result", "Результат: " },
 { "exit", "До свидания!" }
 }
 }
	};
	private string currentLanguage;
	public ConsoleCalculator(string greeting)
	{
		_greeting = greeting;
	}
	public void Start()
	{
		Console.WriteLine(_greeting);
		SelectLanguage();
		RunCalculator();
	}
	private void SelectLanguage()
	{
		Console.WriteLine("Choose a language: en (English), ru (Русский)");
		string languageChoice = Console.ReadLine();
		if (translations.ContainsKey(languageChoice))
		{
			currentLanguage = languageChoice;
			Console.WriteLine(translations[currentLanguage]["greeting"]);
		}
		else
		{
			Console.WriteLine("Invalid language. Defaulting to English.");
			currentLanguage = "en";
		}
	}
	private void PrintSupportedOperators()
	{
		Dictionary<string, string> supportedOperators = new()
 {
 { "+", "Add" },
 { "-", "Subtract" },
 { "*", "Multiply" },
 { "/", "Divide" },
 { "//", "Integer Divide" },
 { "%", "Remainder" },
 { "sqrt", "Square Root" },
 { "log", "Logarithm" }
 };
		Console.WriteLine("Operator choices are as follows:");
		foreach (var op in supportedOperators)
		{
			Console.WriteLine($"{op.Value}: {op.Key}");
		}
	}
	private double GetNumberFromUser()
	{
		double number;
		while (true)
		{
			string input = Console.ReadLine();
			if (double.TryParse(input, out number))
			{
				return number;
			}
			else
			{
				Console.WriteLine("Invalid input. Please enter a valid number.");
		    }
		}
	}
	private void RunCalculator()
	{
		PrintSeparator();
		while (true)
		{
			PrintSupportedOperators();
			string operatorChoice = Console.ReadLine();
			if (operatorChoice == "exit")
			{
				Console.WriteLine(translations[currentLanguage]["exit"]);
				break;
			}
			double firstNumber = GetNumberFromUser();
			double secondNumber = 0;
			if (operatorChoice != "sqrt" && operatorChoice != "log")
			{
				Console.WriteLine("Enter the second number:");
				secondNumber = GetNumberFromUser();
			}
			double result = Calculate(firstNumber, secondNumber,
		   operatorChoice);
			if (operatorChoice == "history")
			{
				ShowHistory();
			}
			Console.WriteLine($"{translations[currentLanguage]["result"]}{ result: F2}");
	    PrintSeparator();
		}
	}
	private double Calculate(double firstNumber, double secondNumber, string
   operatorChoice)
	{
		try
		{
			switch (operatorChoice)
			{
				case "+": return firstNumber + secondNumber;
				case "-": return firstNumber - secondNumber;
				case "*": return firstNumber * secondNumber;
				case "/":
					return secondNumber != 0 ? firstNumber / secondNumber
			   : 0;
				case "//":
					return secondNumber != 0 ? (int)(firstNumber /
			   secondNumber) : 0;
				case "%":
					return secondNumber != 0 ? firstNumber % secondNumber
			   : 0;
				case "sqrt": return Math.Sqrt(firstNumber);
				case "log": return Math.Log(firstNumber);
				default: return 0;
			}
		}
		catch
		{
			return 0;
		}
	}
	private void ShowHistory()
	{
		Console.WriteLine("History of calculations:");
		foreach (var entry in history)
		{
			Console.WriteLine(entry);
		}
	}
	private void PrintSeparator()
	{
		Console.WriteLine("_______");
	}
}
public class Program
{
	public static void Main()
	{
		ConsoleCalculator calculator = new ConsoleCalculator("Welcome to Calculator!");
		calculator.Start();
	}
}
