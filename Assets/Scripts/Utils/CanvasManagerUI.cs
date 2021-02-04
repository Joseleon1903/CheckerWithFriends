using Assets.Scripts.General;
using UnityEngine;

public class CanvasManagerUI : Singleton<CanvasManagerUI>
{

    [SerializeField] private GameObject AlertCanvas;

    [SerializeField] private GameObject playerControlCanvas;

    [SerializeField] private GameObject ProfilePlayerCanvas;

    [SerializeField] private GameObject EndGameCanvas;

    [SerializeField] private GameObject OptionCanvas;

    private void Start()
    {
        playerControlCanvas.SetActive(false);
        ProfilePlayerCanvas.SetActive(false);
        EndGameCanvas.SetActive(false);
    }

    public void StartGameCanvasView() {

        playerControlCanvas.SetActive(true);

        ProfilePlayerCanvas.SetActive(true);

    }


    public void ShowAlertText(string text) {
        AlertCanvas.SetActive(true);
        AlertCanvas.GetComponent<AlertBehavour>().ShowAlert(text);
    }

    public void ShowGameOverCanvas()
    {
        EndGameCanvas.SetActive(true);
    }

    public void ShowGameOptionMenu() {
        OptionCanvas.SetActive(true);
    }
}
