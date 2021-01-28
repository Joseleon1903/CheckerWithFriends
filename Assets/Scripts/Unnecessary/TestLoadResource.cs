using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadResource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        string key = "Blue_Female_Hero";
        Sprite avatar = Resources.Load<Sprite>("Sprites/Profile/" + key);

        if (avatar != null)
        {

            Debug.Log("Sprite found");
        }
        else {
            Debug.Log("Not Sprite found");
        }
    }

    
}
