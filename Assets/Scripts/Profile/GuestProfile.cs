using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using UnityEngine;

public class GuestProfile : BaseProfile
{
    public string profileName { get; private set; }
    public string profileId { get; private set; }
    public string profileNationality { get; private set; }

    [SerializeField]private Sprite profilePicture;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        profileName = ProfileUtil.GenerateGuestName(5);
        profileId = ProfileUtil.GenerateRandomCode(6);
        profileNationality = "Dominican Republic";
    }
}
