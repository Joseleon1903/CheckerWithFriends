using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;

public class MultiplayerBoardAnimation : MonoBehaviour
{

    [SerializeField] private GameObject board;

    void Start()
    {
        Animation();

        InvokeRepeating("Animation", 20.5f, 1.0f);
        
    }

    private void Animation() {

        LeanTween.rotateAround(board, Vector3.up, 720, 20f);
    }

   
}
