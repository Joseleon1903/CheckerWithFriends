using UnityEngine;

public class ChangeButtonBehavour : MonoBehaviour
{
   [SerializeField]private GameObject chessPanel;

    [SerializeField] private GameObject checkerPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeView() {

        if (chessPanel.activeSelf)
        {

            chessPanel.SetActive(false);
            checkerPanel.SetActive(true);
        }
        else
        {
            chessPanel.SetActive(true);
            checkerPanel.SetActive(false);
        }
    
    }

   
}
