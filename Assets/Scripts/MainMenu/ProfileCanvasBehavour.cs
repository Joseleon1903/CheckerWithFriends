using UnityEngine;

public class ProfileCanvasBehavour : MonoBehaviour
{

    [SerializeField] private GameObject optionMenu;

    [SerializeField] private GameObject profileMenu;


    public void PressOptionMenu() {
        Debug.Log("Press option button");

        Instantiate(optionMenu);
    }

    public void PressProfileMenu()
    {
        Debug.Log("Press profile button");

        Instantiate(profileMenu);
    }



}
