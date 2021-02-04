using Assets.Scripts.Utils;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollViewContentBehavour : MonoBehaviour
{
    [SerializeField] private Text  scrolltittle;

    [SerializeField] private Text scrolldescription; 

    private MainMenuActionType actionButton;


    public void RefreshContent(string tittle, string description, MainMenuActionType action) {
        scrolltittle.text = tittle;
        scrolldescription.text = description;
        actionButton = action;

        LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0f);

        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 2f).setEase(LeanTweenType.easeOutBounce);

    }

    public void PressPlayButton(int menuAction) {

        MainMenuActionType action = EnumHelper.GetEnumValue<MainMenuActionType>(menuAction);
        if (menuAction == 6)
        {
            action = actionButton;
        }
        switch (action) {

            default:
            case MainMenuActionType.CheckerPlayButton:

                Debug.Log("Press Play Checker");

                SceneManager.LoadScene("CheckerMultiplayerScene");


                break;

            case MainMenuActionType.CheckerRulesButton:

                Debug.Log("Press Rules Checker");


                break;

            case MainMenuActionType.FriendsButton:

                Debug.Log("Press Friends");


                break;

        }
    
    
    }

}
