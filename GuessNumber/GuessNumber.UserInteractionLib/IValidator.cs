using GuessNumber.CommonLib;

namespace GuessNumber.UserInteractionLib;

public interface IValidator
{
	(bool isSuccess, string? errorMessage) ValidateNumberInput(string? input, out int enteredNumber);

	(bool isSuccess, string? errorMessage) ValidateNumberValue(int enteredValue, (int LowerValue, int UpperValue) interval);

	(bool isSuccess, string? errorMessage) ValidateEnteredNumber(string input, GameSettings settings, out int enteredNumber);

	(bool isSuccess, string? errorMessage) ValidatePositiveNumberInput(string? input, out int enteredPositiveNumber);

	(bool isSuccess, string? errorMessage) ValidateInterval(int lowerValue, int upperValue);
}