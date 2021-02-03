using Assets.Scripts.Network.Service;
using Proyecto26;
using RSG;
using UnityEngine;

public class PublicLobbyService : IService
{

    public static string GetPublicLobbyPath = "/checker/lobby";


    public override IPromise<string> Get(string api)
    {
        throw new System.NotImplementedException();
    }

    public IPromise<PublicAvaliableGameObject[]> GetPublicLobbys(string api, int count)
    {
        //add request param count
        api += "?count="+ count;
        Debug.Log("Post Profile: " + api);

        var promise = new Promise<PublicAvaliableGameObject[]>();
        OnRequestError errorHandler = new OnRequestError(HandlerDefaultRequestError);

        RestClient.GetArray<PublicAvaliableGameObject>(api).
            Then(Response =>
            {
                Debug.Log("Response" + Response);
                promise.Resolve(Response);
            }).Catch(error => errorHandler(error)); ;

        return promise;
    }

}
