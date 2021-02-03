using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_NAME, "Mario Pallino");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_FRAME, "Frame_Blue_Profile");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_AVATAR, "Violet_Female_Hero");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_COUNTER_WON, "0");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_COUNTER_COIN, "200");

        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_NAME, "Mario Pallino");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_FRAME, "Frame_Green_Profile");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_AVATAR, "Yellow_Female_Hero");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_COUNTER_WON, "1");
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_COUNTER_COIN, "200");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckerGameManager.Instance.GameState.Checkmate(PlayerType.P2);
            Debug.Log("Show Game Over Canvas");
            CanvasManagerUI.Instance.ShowGameOverCanvas();

        }

    }
}
