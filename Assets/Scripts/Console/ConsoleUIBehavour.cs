using UnityEngine;
using UnityEngine.UI;

public class ConsoleUIBehavour : MonoBehaviour
{

    public static ConsoleUIBehavour Instance;

    [SerializeField] private Text consoleText;

    private string text = "";

    private void Awake()
    {
        Instance = this;   
    }

    public void LogWhite(string line) {
        text = "<color=white>White: ";
        text += line +"</color>" + "\n";
        consoleText.text += text;
    }

    public void LogBlack(string line)
    {
        text = "Balck: ";
        text +=  line+ "\n";
        consoleText.text += text;
    }

    public void LogMachine(string line)
    {
        text = "<color=red>Machine: ";
        text += line + "</color>" + "\n";
        consoleText.text += text;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W)) {

            LogWhite("Rook f4 ");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LogBlack("King f4 ");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            LogMachine("White is checked");
        }

    }
}
