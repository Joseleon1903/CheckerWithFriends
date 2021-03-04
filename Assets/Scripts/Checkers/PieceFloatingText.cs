using System.Collections;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;

public class PieceFloatingText : MonoBehaviour
{
    [SerializeField] private float disableTime = 1.2f;

    public static float Xoffset = 0.2f;

    public static float Yoffset = 0.2f;

    public static float Zoffset = 0.2f;

    void Start()
    {
        LeanTween.rotateX(gameObject, 30f, 0.3f);

        StartCoroutine(DisableAfterTime(disableTime));
    }

    IEnumerator DisableAfterTime(float time) {

        yield return  new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

}
