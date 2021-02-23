using UnityEngine;
using UnityEngine.UI;

public class JoinErrorPanelBehavour : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Text label;

    private float lastAlert;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        label = GetComponentInChildren<Text>();
    }

    void Update()
    {

        if (Time.time - lastAlert > 0.1f)
        {
            canvasGroup.alpha = 2 - ((Time.time - lastAlert) - 0.1f);
            if (Time.time - lastAlert > 2f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ChangeMessage(string message)
    {
        label.text = message;
        lastAlert = Time.time;
        canvasGroup.alpha = 1;
    }
}
