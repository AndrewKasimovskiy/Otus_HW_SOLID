namespace GuessNumber.UserInteractionLib.Impl;

public class UserConsoleInteraction : IUserInteraction
{
	public void WriteMessage(string message)
		=> Console.WriteLine(message);

	public void WriteLabel(string label) => Console.Write(label);

	public void WriteError(string errorMessage)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(errorMessage);
		Console.ForegroundColor = ConsoleColor.White;
	}

	public void WriteWarning(string warningMessage)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine(warningMessage);
		Console.ForegroundColor = ConsoleColor.White;
	}

	public string Read()
		=> Console.ReadLine() ?? string.Empty;
}
