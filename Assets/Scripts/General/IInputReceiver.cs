namespace Assets.Scripts.General
{
	public enum InputActionType
	{
		GRAB_PIECE = 0,
		PLACE_PIECE = 1,
		CANCEL_PIECE = 2,
		ZOOM_IN = 3,
		ZOOM_OUT = 4,
		ROTATE = 5,
		STOP_ROTATE = 6,
	}

	public interface IInputReceiver
	{
		void EnableInput();
		void DisableInput();
		void OnInputEvent(InputActionType action);
	}
}
