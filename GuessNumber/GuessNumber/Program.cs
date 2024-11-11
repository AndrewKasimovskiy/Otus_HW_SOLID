using Microsoft.Extensions.Configuration;

namespace GuessNumber;

internal class Program
{
	static void Main(string[] args)
	{
		//int try_count = 3;
		//int lowerValue = 0;
		//int upperValue = 10;

		IConfiguration config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true, true)
			.Build();

		var parametr1 = config["NumberOfAttempts"];
		if(!int.TryParse(parametr1, out int numberOfAttempts))
		{
			throw new ArgumentException(nameof(parametr1));
		}

		var parametr2 = config["LowerValue"];
		if (!int.TryParse(parametr2, out int lowerValue))
		{
			throw new ArgumentException(nameof(parametr2));
		}

		var parametr3 = config["UpperValue"];
		if (!int.TryParse(parametr3, out int upperValue))
		{
			throw new ArgumentException(nameof(parametr3));
		}

		Random random = new Random();

		int hiddenNumber = random.Next(lowerValue, upperValue + 1);
		Console.WriteLine($"Загаданное число лежит в диапазоне от {lowerValue} до {upperValue} включительно."
			+ $"\nУ вас есть {numberOfAttempts} попытки чтобы отгадать его.");

		while (numberOfAttempts > 0)
		{
			Console.Write("Введите число, что бы угадать его: ");
			int tryNumber = int.Parse(Console.ReadLine() ?? "");
			if(tryNumber == hiddenNumber)
			{
				break;
			}
			else
			{
				numberOfAttempts--;
				if(tryNumber < hiddenNumber)
				{
					Console.WriteLine("Загаданное число больше вашего");
				}
				else
				{
					Console.WriteLine("Загаданное число меньше вашего");
				}
			}
		}

		if (numberOfAttempts > 0)
		{
			Console.WriteLine($"Поздравляем! Вы угадали, это число {hiddenNumber}");
		}
		else
		{
			Console.WriteLine($"К сожалению вы израсходовали все попытки. Загаданное число {hiddenNumber}");
		}
	}
}
