using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;

public class PlayerProfileAnimation : MonoBehaviour
{

    [SerializeField]private GameObject panelProfile;

    private void OnEnable()
    {
        LeanTween.scale(panelProfile, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutElastic);
    }

    private void OnDisable()
    {
        
    }
}
