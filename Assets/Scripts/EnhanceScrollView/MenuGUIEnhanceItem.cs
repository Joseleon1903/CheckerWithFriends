using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MenuGUIEnhanceItem : EnhanceItem
{

    [SerializeField] private string TittleName;

    [SerializeField] private string DescriptionName;

    [SerializeField] private GameObject ContentView;

    [SerializeField] private Material colorMaterialPanel;

    [SerializeField] private MainMenuActionType menuAction;

    [SerializeField] private MainMenuPanelType menuPanel;

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
        ContentView.GetComponent<ScrollViewContentBehavour>().RefreshContent(colorMaterialPanel.color, menuPanel, menuAction);
    }



}
