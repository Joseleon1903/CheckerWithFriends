namespace Assets.Scripts.General
{
	public enum InputActionType
	{
		GRAB_PIECE = 0,
		PLACE_PIECE = 1,
		CANCEL_PIECE = 2,
	}

	public interface IInputReceiver
	{
		void EnableInput();
		void DisableInput();
		void OnInputEvent(InputActionType action);
	}
}
