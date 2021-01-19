using UnityEngine;
using System.Collections;
using Unity.Pandora.Core.Mobile.Animation.Tween;

public class ProfileOptionBehavour : MonoBehaviour
{

    [SerializeField] private GameObject optionPanel;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(optionPanel, new Vector3(0.8f, 0.8f, 0.8f), 1.5f).setEaseOutBounce();
    }

    public void PressCloseBtn()
    {
        Destroy(gameObject);
    }
}
