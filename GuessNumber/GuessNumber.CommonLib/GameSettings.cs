namespace GuessNumber.CommonLib;

public record struct GameSettings(int NumberOfAttempts, (int LowerValue, int UpperValue) Interval);
