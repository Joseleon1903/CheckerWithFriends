using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Assets.Scripts.Utils;
using Assets.Scripts.General;

public class FacebookBehavour : Singleton<FacebookBehavour>
{

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    #region Login / logout
    public void FacebookLogin()
    {
        var permission = new List<string>() { FacebookPermissions.PublicProfile,FacebookPermissions.Email };

        FB.LogInWithReadPermissions(permission, LoginHandler);

    }

    public void LoginHandler(ILoginResult result) {

        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    //after login success

    public void AfterLoginSuccess() { 
    
    
    }

    //after loginf failure
    public void AfterLoginFail()
    {


    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }
    #endregion


    public void FacebookShareLink() 
    {

        FB.ShareLink(new System.Uri("https://developers.facebook.com/docs/unity/gettingstarted"), "Developer Page",
            "Facebook developer documentation",
            new System.Uri("https://geekscrowd.files.wordpress.com/2012/06/facebook-developer.png")); 
    
    }

    #region Inviting
    public void FacebbokGameRequest() {

        FB.AppRequest("Hey come to play this awesome game!", title: "Chess and Checker invitation");
    }
    #endregion

    public void GetFriendsPlayingThisgame() {

        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result => {

            var dictionary = (Dictionary<string, object>) Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendList = (List<object>)dictionary["data"];

            string listFriend = string.Empty;
            foreach (var dic in friendList) 
            {
                listFriend += ((Dictionary<string, object>)dic)["name"];
            }
            Debug.Log("Friend play this game: "+listFriend);
        });
    }
}
