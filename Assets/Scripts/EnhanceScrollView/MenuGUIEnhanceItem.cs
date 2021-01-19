using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MenuGUIEnhanceItem : EnhanceItem
{

    [SerializeField] private string TittleName;

    [SerializeField] private string DescriptionName;

    [SerializeField] private GameObject ContentView;

    [SerializeField] private MainMenuActionType menuAction;

    private Button uButton;
    private RawImage rawImage;
 
    protected override void OnStart()
    {
        rawImage = GetComponent<RawImage>();
        uButton = GetComponent<Button>();
        uButton.onClick.AddListener(OnClickUGUIButton);

        Assert.IsNotNull<string>(TittleName);
        Assert.IsNotNull<string>(DescriptionName);
        Assert.IsNotNull<GameObject>(ContentView);
    }

    public void OnClickUGUIButton()
    {
        OnClickEnhanceItem();
        Debug.Log("button name :"+ menuAction.ToString());

        int option = (int)EnumHelper.GetEnumValue<MainMenuActionType>(menuAction.ToString());
        ContentView.GetComponent<ScrollViewContentBehavour>().PressPlayButton(option);
    }

    // Set the item "depth" 2d or 3d
    protected override void SetItemDepth(float depthCurveValue, int depthFactor, float itemCount)
    {
        int newDepth = (int)(depthCurveValue * itemCount);
        this.transform.SetSiblingIndex(newDepth);
    }

    public override void SetSelectState(bool isCenter)
    {
        if (rawImage == null)
            rawImage = GetComponent<RawImage>();
        rawImage.color = isCenter ? Color.white : Color.gray;

        if (isCenter) {
            OnSelectItem();
        }
    }

    public void OnSelectItem() {
        ContentView.GetComponent<ScrollViewContentBehavour>().RefreshContent(TittleName, DescriptionName, menuAction);
    }



}
