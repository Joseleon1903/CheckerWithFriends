using Assets.Scripts.Json;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

public class RulePanelBehavior : MonoBehaviour
{
    private int currentNumber;

    [SerializeField] private Text number;

    [SerializeField] private Text rulesDescription;

    [SerializeField] private Image imageRules;

    public void SetUpContent(int numberRule) {

        currentNumber = numberRule;

        string jsonR = ResourcesUtil.FindStringJsonFileInResource(ResourcesUtil.JSON_RULES);

        RulePanelModel[] modelArray = JsonHelper.ArrayFromJson<RulePanelModel>(jsonR);

        foreach (RulePanelModel rule in modelArray) 
        {

            if (rule.ruleId.Equals(numberRule))
            {
                number.text = "Rules N. " + numberRule;
                rulesDescription.text = rule.ruleDescription;

                return;
            }
        }


    }


    public void CloseButtonPress() {
        Debug.Log("Close instruction panel");
        Destroy(gameObject);
    }

    public void NextButtonPress()
    {
        Debug.Log("Entering in method NextButtonPress ");

        if (currentNumber <= 8) {
            SetUpContent(currentNumber++);
        }
    }

}
