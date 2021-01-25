using Assets.Scripts.Profile;
using UnityEngine;

public class GuestProfile : BaseProfile
{
    public string _profileName;
    public string _profileId;
    public string _profileNationality;

    [SerializeField] private Sprite _profileAvatarSprite;

    [SerializeField] private Sprite _profileframeSprite;

    public Sprite ProfileAvatarSprite { get { return _profileAvatarSprite; } set { _profileAvatarSprite = value; } }

    public Sprite ProfileFrameSprite { get { return _profileframeSprite; } set { _profileframeSprite = value; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public string ProfileName { get{ return _nameProfile; }}

    public string ProfileId
    { 
        get {

            if (_isGuest)
            {
                return _guestUserId;
            }
            else {
                return _facebookUserId;
            }
        } 
    }

    public string ProfileNationality { get { return "Dominican Republic"; } }

}
