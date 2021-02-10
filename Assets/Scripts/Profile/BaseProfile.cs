using Assets.Scripts.Network.Model;
using Assets.Scripts.Utils;
using System;
using UnityEngine;

namespace Assets.Scripts.Profile
{
    public abstract class BaseProfile : MonoBehaviour
	{
		public long id;
		public string _email;
		public string _nameProfile;
		public string _facebookUserId;
		public string _guestUserId;
		public string _lastTokenInfoString;
		public string _profilePicture;
		public string _cretionDate;
		public string _playerCoins;
		public int _totalCheckerGame;
		public int _totalCheckerGameWin;
		public string _language;
		public string _nationality;
		public bool _isGuest;


		public static GameObject FetchFromModel(GameObject profile,  ProfileModel model) {

			if (EnumHelper.TRUE.Equals(model.isGuest))
			{
				Debug.Log("fetch Guest profile");

				BaseProfile baseProfile = profile.GetComponent<BaseProfile>();

				baseProfile.id = model.id;
				baseProfile._nameProfile = model.name;
				baseProfile._facebookUserId = model.facebookId;
				baseProfile._guestUserId = model.guestUserId;
				baseProfile._lastTokenInfoString = model.lastTokenInfoString;
				baseProfile._profilePicture = model.profilePicture;
				baseProfile._cretionDate = model.creationDate;
				baseProfile._playerCoins = model.playerCoins;
				baseProfile._totalCheckerGame = model.totalCheckerGame;
				baseProfile._totalCheckerGameWin = model.totalCheckerGameWin;
				baseProfile._language = model.language;
				baseProfile._nationality = model.nationality;
				baseProfile._isGuest = EnumHelper.TRUE.Equals(model.isGuest);
			}
			else
			{
				Debug.Log("fetch Facebook profile");

				throw new NotImplementedException();

			}
			return profile;
		}
	}
}