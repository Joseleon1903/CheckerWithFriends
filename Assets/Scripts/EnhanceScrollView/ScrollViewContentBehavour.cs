using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollViewContentBehavour : MonoBehaviour
{

    [SerializeField] private GameObject CheckerMultiplayerPanel;

    [SerializeField] private GameObject CheckerRulesPanel;

    [SerializeField] private GameObject CheckerInviteFriends;


    private MainMenuActionType actionButton;


    public void RefreshContent(Color color, MainMenuPanelType itemCard,  MainMenuActionType action) {

        GetComponent<Image>().color = color;
        actionButton = action;

        switch (itemCard) {

            case MainMenuPanelType.CheckerMultiplayer:

                Debug.Log("Entering in multiplayer game");
                CheckerRulesPanel.SetActive(false);
                CheckerInviteFriends.SetActive(false);
                CheckerMultiplayerPanel.SetActive(true);


                break;

            case MainMenuPanelType.CheckerRules:

                Debug.Log("Entering in Rules game");
                CheckerRulesPanel.SetActive(true);
                CheckerInviteFriends.SetActive(false);
                CheckerMultiplayerPanel.SetActive(false);


                break;

            case MainMenuPanelType.CheckerFriendsinvited:

                Debug.Log("Entering in Rules game");
                CheckerRulesPanel.SetActive(false);
                CheckerInviteFriends.SetActive(true);
                CheckerMultiplayerPanel.SetActive(false);

                break;
        }

        //LeanTween.scale(gameObject, new Vector3(0.8f, 0.8f, 0.8f), 0f);

        //LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 2f).setEase(LeanTweenType.easeOutBounce);

    }

}
