using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckerGameMultiplayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void PressPlayButton() {
        Debug.Log("Entering in method PressPlayButton");
        SceneLoaderController.Instance.LoadSceneWithTransition("CheckerMultiplayerScene", 4.0f);
    }
}
