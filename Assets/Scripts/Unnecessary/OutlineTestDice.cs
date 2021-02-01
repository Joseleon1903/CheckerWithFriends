using Assets.Scripts.Outliner;
using UnityEngine;

public class OutlineTestDice : MonoBehaviour
{
    [SerializeField] private GameObject targetOutline;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetOutline.GetComponent<Outline>().enabled = !targetOutline.GetComponent<Outline>().enabled;
        }
    }
}
