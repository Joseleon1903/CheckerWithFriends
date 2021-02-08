using Proyecto26;
using System;
using UnityEngine;

public class GeolocationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string ipParam = NetworkUtil.LocalIPAddress();
        Debug.Log("Consult Nationality IP: "+ ipParam);

        RestClient.Get("https://api.ipgeolocationapi.com/geolocate").Then(response => {

            var values = JsonUtility.FromJson<GeolocalizationModel>(response.Text);

            Debug.Log("continent: "+ values.continent); 
            Debug.Log("country name: "+ values.name);
            Debug.Log("nationality: " + values.nationality);

        });

    }

}

[Serializable]
public class GeolocalizationModel {

    public string continent;
    public string name;
    public string nationality;
}