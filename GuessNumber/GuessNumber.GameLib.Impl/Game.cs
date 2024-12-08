using GuessNumber.CommonLib;
using GuessNumber.NumberGeneratorLib;
using GuessNumber.UserInteractionLib;

namespace GuessNumber.GameLib.Impl;

public class Game(IGameLauncher gameLauncher, INumberGenerator numberGenerator, IUserInteraction userInteraction, IValidator validator) : IGame
{
	public GameSettings Settings { get; private set; }

	public bool StartGame()
	{
		Settings = gameLauncher.GetGameSettings();
		bool isGuessed;
		int numberOfAttempts = Settings.NumberOfAttempts;
		string attemptWord = GetAttemptWord(numberOfAttempts);
		int hiddenNumber;

		userInteraction.WriteMessage($"Добро пожаловать в игру \"Угадай число\"!\n" +
			$"Программа загадывает число в диапазоне от {Settings.Interval.LowerValue} до {Settings.Interval.UpperValue} включительно.\n" +
			$"У вас есть {Settings.NumberOfAttempts} {attemptWord}, чтобы отгадать число.");
		hiddenNumber = numberGenerator.GenerateNumber(Settings.Interval.LowerValue, Settings.Interval.UpperValue);
		do
		{
			userInteraction.WriteLabel("Введите число: ");
			var input = userInteraction.Read();
			var (validateResult, errorMessage) = validator.ValidateEnteredNumber(input, Settings, out int enteredNumber);
			if (!validateResult)
			{
				userInteraction.WriteError(errorMessage ?? "Ошибка!!!");
				userInteraction.WriteMessage("Попробуйте ввести число снова...");
				continue;
			}

			isGuessed = CompareToHiddenNumber(enteredNumber, hiddenNumber);
			
			if(!isGuessed)
			{
				numberOfAttempts--;
				attemptWord = GetAttemptWord(numberOfAttempts);
				userInteraction.WriteWarning($"***!У вас осталось {numberOfAttempts} {attemptWord}!***");
			}
			else
				return true;

		} while (numberOfAttempts > 0);

		return false;
	}

	private string GetAttemptWord(int numberOfAttempts)
	{
		byte lastDigitByNumberOfAttempts = (byte)(numberOfAttempts % 10);
		if ((numberOfAttempts >= 11 && numberOfAttempts <= 19)
			|| lastDigitByNumberOfAttempts == 0
			|| (lastDigitByNumberOfAttempts >= 5 && lastDigitByNumberOfAttempts <= 9))
			return "попыток";
		else if (lastDigitByNumberOfAttempts == 1)
			return "попытка";
		else
			return "попытки";
	}

	private bool CompareToHiddenNumber(int enteredNumber, int hiddenNumber)
	{
		int compareResult = enteredNumber.CompareTo(hiddenNumber);

		if (compareResult > 0)
		{
			userInteraction.WriteWarning($"Загаданное число меньше вашего.");
			return false;
		}
		else if (compareResult < 0)
		{
			userInteraction.WriteWarning($"Загаданное число больше вашего.");
			return false;
		}

		return true;
	}
}
