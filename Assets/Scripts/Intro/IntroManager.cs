using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Intro
{
    public class IntroManager : MonoBehaviour
    {

        [SerializeField]private GameObject guestProfile;

        public void LoginGuestButton() {

            Debug.Log("Entering in method LoginGuestButton");

            //instanciate a profile guest
            Instantiate(guestProfile);

            Debug.Log("Load main scene");

            PlayerPrefs.SetString(PlayerPreferenceKey.GUEST_PROFILE_KEY, EnumHelper.TRUE);

            SceneManager.LoadScene("ProfileCustomizationScene");
        
        }
        
    }
}