using Assets.Scripts.Utils;
using RSG;
using System;
using UnityEngine;

namespace Assets.Scripts.Network.Service
{
    public abstract class IService
    {
        protected delegate void OnRequestError(Exception var);

        public abstract IPromise<string> Get(string api);



        /// <summary>
        /// 
        /// default request error handler
        /// 
        /// </summary>
        public virtual void HandlerDefaultRequestError(Exception response)
        {
            Debug.Log("Error" + response.Message);

            PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TITTLE, "Server conncetion error");
            PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TEXT, "An error occurred trying to connect with the server, please check your internet connection");
            GameObject.FindObjectOfType<RestClientBehavour>().ShowMessageError();
        }
    }
}