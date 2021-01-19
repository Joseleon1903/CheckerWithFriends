using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LandscapeCamera : MonoBehaviour
{

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SettingCameraDefaultConfigrutarion();
    }

    private void SettingCameraDefaultConfigrutarion() {

        Debug.Log("White Camera Configuration ");

        Vector3 position = new Vector3(-12,11,5);

        Vector3 scale = Vector3.one;

        Quaternion rotation = Quaternion.Euler(32, -270, 0);

        _camera.transform.position = position;

        _camera.transform.localScale = scale;

        _camera.transform.localRotation = rotation;
    }
   
}
