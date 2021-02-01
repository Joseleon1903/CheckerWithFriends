using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UIExtention
{
    public class InGameOptionUIBehavour : MonoBehaviour
    {

        [SerializeField] private GameObject optionPanel;

        private void OnEnable()
        {
            LeanTween.scale(optionPanel, Vector3.one, 2.5f).setEase(LeanTweenType.easeOutElastic);
        }

        private void OnDisable()
        {

        }

        public void PressResumenGameButton()
        {
            gameObject.SetActive(false);
        }

        public void PressExitGameButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
