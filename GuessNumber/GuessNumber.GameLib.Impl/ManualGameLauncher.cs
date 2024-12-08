using GuessNumber.CommonLib;
using GuessNumber.UserInteractionLib;

namespace GuessNumber.GameLib.Impl;

public class ManualGameLauncher(IUserInteraction userInteraction, IValidator validator) : GameLauncher(validator)
{
	public override GameSettings GetGameSettings()
	{
		int numberOfAttempts = SetNumberOfAttempts();
		(int, int) interval = SetInterval();

		return new GameSettings(numberOfAttempts, interval);
	}

	private int SetNumberOfAttempts()
	{
		int numberOfAttempts;
		(bool isSuccess, string? errMessage) validationResult;
		do
		{
			userInteraction.WriteLabel("Введите количество попыток, которые вы можете использовать в игре: ");
			var input = userInteraction.Read();
			validationResult = validator.ValidatePositiveNumberInput(input, out numberOfAttempts);
			if (!validationResult.isSuccess)
			{
				userInteraction.WriteError(validationResult.errMessage ?? "Ошибка");
				userInteraction.WriteMessage("Попробуйте ввести значение заново...");
			}
		} while (!validationResult.isSuccess);
		return numberOfAttempts;
	}

	private int SetLowerValue()
	{
		int lowerValue;
		(bool isSuccess, string? errMessage) validationResult;
		do
		{
			userInteraction.WriteLabel("Введите нижнюю границу диапазона для загадываемого числа: ");
			var input = userInteraction.Read();
			validationResult = validator.ValidateNumberInput(input, out lowerValue);
			if (!validationResult.isSuccess)
			{
				userInteraction.WriteError(validationResult.errMessage ?? "Ошибка");
				userInteraction.WriteMessage("Попробуйте ввести значение заново...");
			}
		} while (!validationResult.isSuccess);
		return lowerValue;
	}

	private int SetUpperValue()
	{
		int upperValue;
		(bool isSuccess, string? errMessage) validationResult;
		do
		{
			userInteraction.WriteLabel("Введите верхнюю границу диапазона для загадываемого числа: ");
			var strLowerValue = userInteraction.Read();
			validationResult = validator.ValidateNumberInput(strLowerValue, out upperValue);
			if (!validationResult.isSuccess)
			{
				userInteraction.WriteError(validationResult.errMessage ?? "Ошибка");
				userInteraction.WriteMessage("Попробуйте ввести значение заново...");
			}
		} while (!validationResult.isSuccess);
		return upperValue;
	}

	private (int, int) SetInterval()
	{
		int lowerValue, upperValue;
		(bool isSuccess, string? errMessage) validationResult;
		do
		{
			lowerValue = SetLowerValue();
			upperValue = SetUpperValue();
			validationResult = validator.ValidateInterval(lowerValue, upperValue);
			if(!validationResult.isSuccess)
			{
				userInteraction.WriteError(validationResult.errMessage ?? "Ошибка");
				userInteraction.WriteMessage("Попробуйте ввести значение заново...");
			}
		} while (!validationResult.isSuccess);
		return (lowerValue, upperValue);
	}
}
