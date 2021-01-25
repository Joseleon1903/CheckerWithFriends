using Assets.Scripts.Network.Model;
using Proyecto26;
using RSG;
using System;
using UnityEngine;

public class ApiVersionService : IService
{
    public static string VersionPath = "/application/version";

    public delegate void OnRequestError(Exception var);

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

    /// <summary>
    /// 
    /// default request error handler
    /// 
    /// </summary>
    /// <param name="response"></param>
    public void HandlerDefaultRequestError(Exception response)
    {
        Debug.Log("Error" + response.Message);
    }

}
