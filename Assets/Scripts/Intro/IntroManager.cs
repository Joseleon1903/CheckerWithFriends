using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Intro
{
    public class IntroManager : MonoBehaviour
    {

        [SerializeField]private GameObject guestProfile;

        public void LoginGuestButton() {

            LoggerFile.Instance.DEBUG_LINE("Entering in method LoginGuestButton");

            //instanciate a profile guest
            Instantiate(guestProfile);

            LoggerFile.Instance.DEBUG_LINE("Load main scene");
            SceneManager.LoadScene("MainMenu");
        
        }

        
    }
}