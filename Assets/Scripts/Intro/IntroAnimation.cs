using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{

    [SerializeField] GameObject chessPiece;

    [SerializeField] GameObject checkerPiece;

    private float jumpDistance = 0.7f;

    void Start()
    {

        InvokeRepeating("AnimationChess", 1.0f, 5.0f);

        InvokeRepeating("AnimationChecker", 3.0f, 5.0f);

    }


    private void AnimationChess() {

        var seq = LeanTween.sequence();

        seq.insert(LeanTween.moveY(chessPiece, jumpDistance, 1f).setEase(LeanTweenType.linear));
        seq.insert(LeanTween.moveY(chessPiece, 0f, 1f).setEase(LeanTweenType.easeOutBounce));

        seq.insert(LeanTween.rotateAround(chessPiece, Vector3.up, 40.0f, 5f));

    }

    private void AnimationChecker()
    {

        var seq = LeanTween.sequence();

        seq.insert(LeanTween.moveY(checkerPiece, jumpDistance, 1f).setEase(LeanTweenType.linear));
        seq.insert(LeanTween.moveY(checkerPiece, 0f, 1f).setEase(LeanTweenType.easeOutBounce));

        seq.insert(LeanTween.rotateAround(checkerPiece, Vector3.right, 60.0f, 5f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
