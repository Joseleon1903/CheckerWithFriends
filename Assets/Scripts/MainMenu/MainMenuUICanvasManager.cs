using Assets.Scripts.General;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MainMenu
{
    public class MainMenuUICanvasManager : Singleton<MainMenuUICanvasManager>
    {

        [SerializeField] private GameObject settingCanvas;

        [SerializeField] private GameObject profileCanvas;

        private void Awake()
        {
            settingCanvas.SetActive(false);
        }


        public void ShowSettingCanvas() {
            settingCanvas.SetActive(true);
        }



       
    }
}