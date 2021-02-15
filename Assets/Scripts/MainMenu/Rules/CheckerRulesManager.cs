using UnityEngine;

public class CheckerRulesManager : MonoBehaviour
{

    [SerializeField] private GameObject canvasInstructionPrefab;


    public void ShowRulesNumber(int numberRule) {

        Debug.Log("Show Rule number : "+ numberRule);

        var objectCanvas = Instantiate(canvasInstructionPrefab);

        objectCanvas.GetComponent<RulePanelBehavior>().SetUpContent(numberRule);
    
    }
}
