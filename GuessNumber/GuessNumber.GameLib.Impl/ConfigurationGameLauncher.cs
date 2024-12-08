using GuessNumber.CommonLib;
using GuessNumber.UserInteractionLib;
using Microsoft.Extensions.Configuration;

namespace GuessNumber.GameLib.Impl;

public class ConfigurationGameLauncher(string filePath, IConfigurationBuilder configurationBuilder, IWriter writer, IValidator validator) 
	: GameLauncher(validator)
{
	private readonly IConfiguration _configuration = configurationBuilder.AddJsonFile(filePath).Build();

	public override GameSettings GetGameSettings()
	{
		try
		{
			int numberOfAttempts = SetNumberOfAttempts();
			(int, int) interval = SetInterval();
			return new GameSettings(numberOfAttempts, interval);
		}
		catch(Exception ex)
		{
			writer.WriteError(ex.Message);
			throw;
		}
	}

	private int SetNumberOfAttempts()
	{
		int numberOfAttempts;
		(bool isSuccess, string? errMessage) validationResult;
		var strNumberOfAttempts = _configuration["NumberOfAttempts"];
		validationResult = validator.ValidatePositiveNumberInput(strNumberOfAttempts, out numberOfAttempts);
		if (!validationResult.isSuccess)
			throw new Exception(validationResult.errMessage);
		return numberOfAttempts;
	}

	private (int, int) SetInterval()
	{
		int lowerValue = SetLowerValue();
		int upperValue = SetUpperValue();
		var validationResult = validator.ValidateInterval(lowerValue, upperValue);
		if (!validationResult.isSuccess)
			throw new Exception(validationResult.errorMessage);
		return (lowerValue, upperValue);
	}

	private int SetLowerValue()
	{
		int lowerValue;
		(bool isSuccess, string? errMessage) validationResult;
		var strLowerValue = _configuration["LowerValue"];
		validationResult = validator.ValidateNumberInput(strLowerValue, out lowerValue);
		if (!validationResult.isSuccess)
			throw new Exception(validationResult.errMessage);
		return lowerValue;
	}

	private int SetUpperValue()
	{
		int upperValue;
		(bool isSuccess, string? errMessage) validationResult;
		var strUpperValue = _configuration["UpperValue"];
		validationResult = validator.ValidateNumberInput(strUpperValue, out upperValue);
		if (!validationResult.isSuccess)
			throw new Exception(validationResult.errMessage);
		return upperValue;
	}
}