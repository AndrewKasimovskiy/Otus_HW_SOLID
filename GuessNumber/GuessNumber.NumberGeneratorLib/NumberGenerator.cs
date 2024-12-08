namespace GuessNumber.NumberGeneratorLib;

public abstract class NumberGenerator : INumberGenerator
{
	public abstract int GenerateNumber(int lowerValue, int upperValue);
}
