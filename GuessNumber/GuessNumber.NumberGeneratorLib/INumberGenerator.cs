namespace GuessNumber.NumberGeneratorLib;

public interface INumberGenerator
{
	int GenerateNumber(int lowerValue, int upperValue);
}