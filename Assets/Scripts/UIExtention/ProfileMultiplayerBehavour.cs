using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMultiplayerBehavour : MonoBehaviour
{

    [SerializeField] private GameObject profileAvatarImage;

    [SerializeField] private GameObject profileFrameImage;

    private GameObject profile;

    private void Awake()
    {
        Debug.Log("Entering awake in profile Multiplayer canvas behavour");

        profile = Finder.FindGameProfile();
    }

    private void Start()
    {
        ProfileUtil.SetUpProfileImage(profile, profileAvatarImage, profileFrameImage);
    }


    public void PressBackButton() {

        Debug.Log("Press Back Button");

        SceneManager.LoadScene("MainMenu");

        if (FindObjectOfType<ClientWSBehavour>() != null) {
            var client = FindObjectOfType<ClientWSBehavour>();
            Destroy(client);
        }

        if (FindObjectOfType<ServerBehavour>() != null)
        {
            var server = FindObjectOfType<ServerBehavour>();
            Destroy(server);
        }
    }

}
