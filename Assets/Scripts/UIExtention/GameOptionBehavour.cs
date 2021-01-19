using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;

public class GameOptionBehavour : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    void Start()
    {
        LeanTween.scale(optionPanel, new Vector3(0.8f, 0.8f, 0.8f), 1.5f).setEaseOutBounce();
    }

    public void PressCloseBtn() {
        Destroy(gameObject);
    } 
}
