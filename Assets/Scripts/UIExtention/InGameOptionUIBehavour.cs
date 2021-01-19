using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UIExtention
{
    class InGameOptionUIBehavour : MonoBehaviour
    {

        public void PressExitGameButton() {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
