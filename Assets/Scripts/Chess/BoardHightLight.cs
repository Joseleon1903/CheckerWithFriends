using UnityEngine;

public class BoardHightLight : MonoBehaviour
{
    public static BoardHightLight Intance { get; set; }

    [SerializeField] private GameObject HightLightprefab;

    [SerializeField] private GameObject CaptureHightLightprefab;

    [SerializeField]private Material checkedMaterial;

    private Material previusMat;

    private float offset = 0.01f;

    private void Start()
    {
        Intance = this;
    }

    private GameObject GetHightLightObject()
    {
        GameObject light = Instantiate(HightLightprefab);
        light.SetActive(false);
        return light;
    }

    private GameObject GetCaptureHightLightObject()
    {
        GameObject light = Instantiate(CaptureHightLightprefab);
        light.SetActive(false);
        return light;
    }

    public void HightLightKingChecked(int x , int y) {
        ChessBehaviour king = ChessBoarderManager.Instance.ChessTable[x, y];
        previusMat = king.GetComponent<Renderer>().material;
        king.GetComponent<MeshRenderer>().material = checkedMaterial;
    }

    public void HightLightAllowedMoves(bool [,] moves) 
    {

        for (int i =0; i< 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i,j]) 
                {
                    GameObject lightObject = ChossedHighlight(i,j);
                    lightObject.SetActive(true);
                    lightObject.transform.position = new Vector3(i+0.4f, offset, j+0.5f);
                }
            }
        }
    }

    public void HidenHightLight()
    {
        GameObject[] hightLights = GameObject.FindGameObjectsWithTag("Tiled");
        foreach (GameObject obj in hightLights)
            Destroy(obj);
    }

    private GameObject ChossedHighlight(int x , int y) {

        ChessBehaviour piece = ChessBoarderManager.Instance.ChessTable[x, y];
        GameObject gameObjectOut;
        if (piece == null)
        {
            gameObjectOut = GetHightLightObject();
        }
        else
        {
            gameObjectOut = GetCaptureHightLightObject();
        }
        return gameObjectOut;
    }
    
}
