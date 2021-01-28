using Assets.Scripts.Network.Service;
using Proyecto26;
using RSG;
using UnityEngine;

public class ApiVersionService : IService
{
    public static string VersionPath = "/application/version";

    public override IPromise<string> Get(string api)
    {
        // Create promise.
        var promise = new Promise<string>();
        OnRequestError errorHandler = new OnRequestError(HandlerDefaultRequestError);

        RestClient.Get("http://localhost:8085/application/version").Then(response =>
        {
            Debug.Log("Process Response");
            Debug.Log(response.Text);
            promise.Resolve(response.Text);
        }).Catch( error => errorHandler(error));

        return promise; // Return the promise so the caller can await resolution (or error). \
    }

}
