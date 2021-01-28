using Assets.Scripts.Network.Model;
using Proyecto26;
using RSG;
using System;
using UnityEngine;

namespace Assets.Scripts.Network.Service
{

    public class ProfileService : IService
    {

        public static string CreateProfilePath = "/user/profile/create";

        public override IPromise<string> Get(string api)
        {
            throw new NotImplementedException();
        }

        public IPromise<ProfileModel> PostProfile(string api, ProfileModel param)
        {
            Debug.Log("Post Profile: " + api);

            var promise = new Promise<ProfileModel>();
            OnRequestError errorHandler = new OnRequestError(HandlerDefaultRequestError);

            RestClient.Post<ProfileModel>(api, param).
                Then(Response =>
                {
                    Debug.Log("Response" + Response);
                    promise.Resolve(Response);
                }).Catch(error => errorHandler(error)); ;

            return promise;
        }

    }
}