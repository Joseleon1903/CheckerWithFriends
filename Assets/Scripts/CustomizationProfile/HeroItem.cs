using UnityEngine;
using static CustomUIManager;

public class HeroItem : MonoBehaviour
{
    [SerializeField]
    private AvatarKey _key;

    public AvatarKey ItemKey { get { return _key; } }
}
