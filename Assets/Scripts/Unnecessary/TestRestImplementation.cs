using UnityEngine;

public class TestRestImplementation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string api = RestClientBehavour.Instance.ApiBaseUrl+ ApiVersionService.VersionPath;

        //IService serviceVersion = new ApiVersionService();

        //serviceVersion.Get(api);

    }

}
