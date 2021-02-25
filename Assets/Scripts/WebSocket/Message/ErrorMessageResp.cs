using Assets.Script.WebSocket;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.WebSocket.Message
{
    public class ErrorMessageResp
    {

        public string error;
        public string code;
        public string codeError;

        public ErrorMessageResp(string json) {
            error = string.Empty;
            code = string.Empty;
            codeError = string.Empty;
            ConvertMessage(json);
        }

        public void ConvertMessage(string text) {
            string[] eData = text.Split('|');
            if (eData.Length >= 3)
            {
                this.error = eData[0];
                this.code = eData[1];
                this.codeError = eData[2];
            }
        }


        public void ExecuteBadLobbyCodeError() {

            JoinPrivateMatchBehavour privatematch = GameObject.FindObjectOfType<JoinPrivateMatchBehavour>();

            if (privatematch != null) {
                privatematch.ShowErrorPanel("Invalid lobby code");
            }
        }

        public void ExecuteFullLobbyCodeError()
        {
            Debug.Log("Entering in full lobby error");
            PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TITTLE, "Error connection lobby");
            PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TEXT, "The lobby you are trying to access is full or is no longer available, try again.");

            GameObject.FindObjectOfType<SocketConfig>().ShowMessageError();
        }


    }
}