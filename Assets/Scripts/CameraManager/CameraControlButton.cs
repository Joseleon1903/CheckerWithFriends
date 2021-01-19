using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum CameraMode { 
    CAMERA_BLOCK = 0,
    CAMERA_FREE= 1
}

public class CameraControlButton : MonoBehaviour
{

    [SerializeField]private Sprite[] icons;

    private CameraMode[] cameraMode;

    public CameraMode SelectedCameraMode;

    private Image buttonImg; 

    // Start is called before the first frame update
    void Start()
    {
        cameraMode = (CameraMode[])Enum.GetValues(typeof(CameraMode));
        buttonImg = GetComponent<Image>();
        SelectedCameraMode = CameraMode.CAMERA_BLOCK;
    }


    public void ChangeCameraMode() {
        CameraMode nextMode = NextMode();
        SelectedCameraMode = nextMode;
        int iconN = (int)EnumHelper.GetEnumValue<CameraMode>(nextMode.ToString());
        buttonImg.sprite = icons[iconN];
        switch (SelectedCameraMode) {

            default:
            case CameraMode.CAMERA_FREE:

                Debug.Log("Camera free point of view");

                FindObjectOfType<CameraController>().ActiveOrbitation = true;

                break;

            case CameraMode.CAMERA_BLOCK:

                Debug.Log("Camera block view");

                FindObjectOfType<CameraController>().ActiveOrbitation = false;

                break;

        }

    }

    private CameraMode NextMode() {

        if (SelectedCameraMode == CameraMode.CAMERA_FREE) {
            return CameraMode.CAMERA_BLOCK;
        } else if (SelectedCameraMode == CameraMode.CAMERA_BLOCK) {
            return CameraMode.CAMERA_FREE;
        }
        return CameraMode.CAMERA_BLOCK;
    }
   
}