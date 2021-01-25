using UnityEngine;

namespace Assets.Scripts.Profile
{
    public class FacebookProfile : BaseProfile
    {
        public string profileName { get; private set; }
        public string profileId { get; private set; }
        public string profileNationality { get; private set; }

        [SerializeField] private Sprite profilePicture;

        [SerializeField] private Sprite _profileAvatarSprite;

        [SerializeField] private Sprite _profileframeSprite;

        public Sprite ProfileAvatarSprite { get { return _profileAvatarSprite; } set { _profileAvatarSprite = value; } }

        public Sprite ProfileFrameSprite { get { return _profileframeSprite; } set { _profileframeSprite = value; } }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public string ProfileName { get { return _nameProfile; } }

        public string ProfileId
        {
            get
            {

                if (_isGuest)
                {
                    return _guestUserId;
                }
                else
                {
                    return _facebookUserId;
                }
            }
        }

        public string ProfileNationality { get { return "Dominican Republic"; } }
    }
}