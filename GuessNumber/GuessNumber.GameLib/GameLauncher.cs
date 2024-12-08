using GuessNumber.CommonLib;
using GuessNumber.UserInteractionLib;

namespace GuessNumber.GameLib;

public abstract class GameLauncher(IValidator validator) : IGameLauncher
{
	protected readonly IValidator validator = validator;

	public abstract GameSettings GetGameSettings();
}
