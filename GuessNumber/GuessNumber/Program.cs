using GuessNumber.GameLib;
using GuessNumber.GameLib.Impl;
using GuessNumber.NumberGeneratorLib;
using GuessNumber.NumberGeneratorLib.Impl;
using GuessNumber.UserInteractionLib;
using GuessNumber.UserInteractionLib.Impl;
using Microsoft.Extensions.Configuration;

namespace GuessNumber;

internal class Program
{
	private IUserInteraction? userInteraction;
	static void Main(string[] args)
	{
		IValidator validator = new Validator();
		IUserInteraction userInteraction = new UserConsoleInteraction();
		
		var gameLauncher = GetGameLauncher(userInteraction, validator);
		var numberGenerator = GetNumberGenerator();

		IGame game = new Game(gameLauncher, numberGenerator, userInteraction, validator);
		bool gameResult = game.StartGame();

		if (gameResult)
			userInteraction.WriteMessage("Поздравляем! Вы угадали загаданное число!");
		else
			userInteraction.WriteMessage("Увы, но вы не смоги отгадать число и потратили все попытки.");
	}

	static IGameLauncher GetGameLauncher(IUserInteraction userInteraction, IValidator validator)
	{
		string filePath = Directory.GetCurrentDirectory() + @"\appsettings.json";
		if (File.Exists(filePath))
		{
			return new ConfigurationGameLauncher(filePath, new ConfigurationBuilder(), userInteraction, validator);
		}
		else
		{
			return new ManualGameLauncher(userInteraction, validator);
		}
	}

	static INumberGenerator GetNumberGenerator()
	{
		return new SimpleNumberGenerator();
	}
}
