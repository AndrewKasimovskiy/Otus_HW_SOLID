namespace GuessNumber.UserInteractionLib;

public interface IWriter
{
	void WriteMessage(string message);

	void WriteLabel(string label);

	void WriteError(string errorMessage);

	void WriteWarning(string warningMessage);
}