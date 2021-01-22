using UnityEngine;

namespace Assets.Scripts.Profile
{
    public abstract class BaseProfile : MonoBehaviour
	{
		private long id;
		private string _facebookUserId;
		private string _lastTokenInfoString;
		private string _profilePicture;
		private string _cretionDate;
		private int _totalCheckerGame;
		private int _totalCheckerGameWin;
		private string _language;
		private string _nationality;
		private bool _isGuest;

	}
}