using UnityEngine;

namespace Assets.Scripts.Profile
{
    public class FacebookProfile : MonoBehaviour
    {
        public string profileName { get; private set; }
        public string profileId { get; private set; }
        public string profileNationality { get; private set; }

        [SerializeField] private Sprite profilePicture;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
         
        }
    }
}