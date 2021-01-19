using Assets.Scripts.Chess;

namespace Assets.Scripts.General
{
	public interface IInputReceiver
	{
		void EnableInput();
		void DisableInput();
		void OnInputEvent(InputActionType action);
	}
}
