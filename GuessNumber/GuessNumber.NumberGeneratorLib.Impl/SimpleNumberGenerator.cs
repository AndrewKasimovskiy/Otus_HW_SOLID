namespace GuessNumber.NumberGeneratorLib.Impl;

public class SimpleNumberGenerator : NumberGenerator
{
	public override int GenerateNumber(int lowerValue, int upperValue)
	{
		int generatedNumber = lowerValue - 1;
		int length = upperValue - lowerValue;
		short sign = (short)(DateTime.UtcNow.Ticks % 2 == 0 ? -1 : 1);
		while (generatedNumber < lowerValue || generatedNumber > upperValue)
		{
			generatedNumber = length / (int)(DateTime.UtcNow.Ticks % 10 + 1) * sign;
		}
		return generatedNumber;
	}
}