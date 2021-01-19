using UnityEngine;
using UnityEngine.UI;

public class AlertBehavour : MonoBehaviour
{
    private Text alertText;

    private bool active;

    private CanvasGroup canvasGroup;

    private float lastAlert;

    private void Awake()
    {
        alertText = GetComponentInChildren<Text>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }


    public void ShowAlert(string text) {
        alertText.text = text;
        lastAlert = Time.time;
        active = true;
    }

    void Update()
    {
        if (active) 
        {
            if (Time.time - lastAlert > 0.1f) 
            {
                canvasGroup.alpha = 2 -( (Time.time - lastAlert) - 0.1f);
                if (Time.time - lastAlert > 3.5f)
                {
                    active = false;
                }

            }
        }  
    }
}
