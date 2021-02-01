using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlAnimation : MonoBehaviour
{
    [SerializeField]private GameObject panelObject;

    [SerializeField] private GameObject cameraViewButton;

    [SerializeField] private Material redColorMat;

    [SerializeField] private Material greenColorMat;

    private bool isSelected = false;

    private void OnEnable()
    {
        // final pos x = 100 , y = -130
        LeanTween.moveX(panelObject, 100, 1.7f).setEase(LeanTweenType.easeOutElastic).setDelay(0.5f);

        LeanTween.scale(panelObject, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutElastic).setDelay(0.5f);

        isSelected = false;
    }

    private void OnDisable()
    {
        
    }

    public void OnClickEnableButton()
    {
        if (!isSelected)
        {
            Image background = cameraViewButton.GetComponent<Image>();
            background.color = redColorMat.color;

            //ability  player zoom en orbit

        }
        else {

            Image background = cameraViewButton.GetComponent<Image>();
            background.color = greenColorMat.color;


            //disable player zoom en orbit

        }
        isSelected = !isSelected;
    }

}
