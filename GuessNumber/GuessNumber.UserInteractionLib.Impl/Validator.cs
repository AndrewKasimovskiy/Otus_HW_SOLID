using GuessNumber.CommonLib;

namespace GuessNumber.UserInteractionLib.Impl;

public class Validator : IValidator
{
	public (bool isSuccess, string? errorMessage) ValidateNumberInput(string? input, out int enteredNumber)
	{
		if (!int.TryParse(input, out enteredNumber))
		{
			string errorMessage = "Введенное значение должно быть числом";
			return (false, errorMessage);
		}

		return (true, null);
	}

	public (bool isSuccess, string? errorMessage) ValidateNumberValue(int enteredValue, (int LowerValue, int UpperValue) interval)
	{
		if (enteredValue < interval.LowerValue || enteredValue > interval.UpperValue)
		{
			string errorMessage = "Ваше число выходит за дипазон загаданного числа. " +
				$"Введенное число должно быть в диапазоне [{interval.LowerValue},{interval.UpperValue}].";
			return (false, errorMessage);
		}

		return (true, null);
	}

	public (bool isSuccess, string? errorMessage) ValidateEnteredNumber(string input, GameSettings settings, out int enteredNumber)
	{
		var (validateResult, errorMessage) = ValidateNumberInput(input, out enteredNumber);
		if (!validateResult)
		{
			return (validateResult, errorMessage);
		}
		else
		{
			return ValidateNumberValue(enteredNumber, settings.Interval);
		}
	}

	public (bool isSuccess, string? errorMessage) ValidatePositiveNumberInput(string? input, out int enteredPositiveNumber)
	{
		enteredPositiveNumber = default;
		int enteredNumber;
		var validateResult = ValidateNumberInput(input, out enteredNumber);
		if (!validateResult.isSuccess)
		{
			return (validateResult.isSuccess, validateResult.errorMessage);
		}

		if(enteredNumber <= 0)
		{
			string errorMessage = "Число должно быть положительным, т.е. больше 0";
			return (false, errorMessage);
		}

		enteredPositiveNumber = enteredNumber;
		return (true, null);
	}

	public (bool isSuccess, string? errorMessage) ValidateInterval(int lowerValue, int upperValue)
	{
		if (lowerValue >= upperValue)
			return (false, $"Верхний порог [{nameof(upperValue)}={upperValue}] должен быть больше нижнего порога [{nameof(lowerValue)}={lowerValue}] в интервале");
		return (true, null);
	}
}