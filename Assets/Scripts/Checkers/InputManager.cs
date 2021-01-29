using Assets.Scripts.General;
using Assets.Scripts.Profile;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

	public delegate void InputEventHandler(InputActionType actionType);
	public static event InputEventHandler InputEvent;

	public Vector2 mouseAxis;

	public Vector2 MouseAxis
	{
		get { return mouseAxis; }
	}

	void Awake()
	{
		mouseAxis = new Vector2(0, 0);
	}

	void OnDisable()
	{
		InputEvent = null;
	}

	public void InvokeInputEvent(InputActionType actionType)
	{
		InputEvent(actionType);
	}

	void Update()
	{
		mouseAxis.x = Input.GetAxis("Mouse X");
		mouseAxis.y = Input.GetAxis("Mouse Y");

		if (InputEvent == null) return;

		if (Input.GetMouseButtonDown(0))
		{
			if (CheckerGameManager.Instance.GameState.IsWaiting)
			{
				InputEvent(InputActionType.GRAB_PIECE);
			}
			else if (CheckerGameManager.Instance.GameState.IsHolding)
			{
				InputEvent(InputActionType.PLACE_PIECE);
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Up button");
		}

		//camera control
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			InputEvent(InputActionType.ZOOM_IN);
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			InputEvent(InputActionType.ZOOM_OUT);
		}

		if (Input.GetMouseButtonDown(2))
		{
			InputEvent(InputActionType.ROTATE);
		}
		else if (Input.GetMouseButtonUp(2))
		{
			InputEvent(InputActionType.STOP_ROTATE);
		}

	}

}