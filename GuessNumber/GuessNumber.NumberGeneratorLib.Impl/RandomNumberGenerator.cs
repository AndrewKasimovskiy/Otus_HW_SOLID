namespace GuessNumber.NumberGeneratorLib.Impl;

public class RandomNumberGenerator : NumberGenerator
{
	public override int GenerateNumber(int lowerValue, int upperValue)
		=> new Random().Next(lowerValue, upperValue + 1);
}
