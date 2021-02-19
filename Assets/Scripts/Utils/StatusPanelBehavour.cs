using UnityEngine;
using UnityEngine.UI;

public class StatusPanelBehavour : MonoBehaviour
{
    private Text alertText;

    private CanvasGroup canvasGroup;

    private float lastAlert;

    private void OnEnable()
    {
        alertText = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        lastAlert = Time.time;
    }

    void Update()
    {
        if (Time.time - lastAlert > 0.1f) 
        {
            canvasGroup.alpha = 2 -( (Time.time - lastAlert) - 0.1f);
            if (Time.time - lastAlert > 2f)
            {
                lastAlert = Time.time;
            }

        }
    }

    public void ChangeMessage(string message ) {

        alertText.text = message;
    }

}
