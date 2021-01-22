using UnityEngine;
using static CustomUIManager;

public class FrameItem : MonoBehaviour
{
    [SerializeField]
    private FrameKey _key;

    public FrameKey ItemKey { get { return _key; } }

}


