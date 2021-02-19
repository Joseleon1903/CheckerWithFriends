using UnityEngine;
using UnityEngine.UI;

public class JoinErrorPanelBehavour : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Text label;

    private float lastAlert;

    private bool isShow;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        label = GetComponentInChildren<Text>();
        isShow = true;
        canvasGroup.alpha = 1;
    }

    void Start()
    {
        lastAlert = Time.time;
    }

    void Update()
    {


        if (Time.time - lastAlert > 0.1f && isShow)
        {
            canvasGroup.alpha = 2 - ((Time.time - lastAlert) - 0.1f);
            if (Time.time - lastAlert > 2f)
            {
                gameObject.SetActive(false);
            }
        }

    }

    public void ShowAlert()
    {



    }
}
