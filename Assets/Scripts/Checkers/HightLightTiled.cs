using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Checkers
{
    class HightLightTiled : MonoBehaviour
    {

        public Vector3 pieceOffSet = new Vector3(0.5f, 0.1f, 0.5f);

        [SerializeField] private GameObject MovePrefabs;

        [SerializeField] private GameObject CapturePrefabs;

        private List<GameObject> InGameHighLiteMove = new List<GameObject>();

        private List<GameObject> InGameHighLiteCapture = new List<GameObject>();

        public static HightLightTiled Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void HightLight(bool[,] lightTiles)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (lightTiles[i, j])
                    {
                        var light = Instantiate(MovePrefabs , transform);
                       
                        light.transform.position = (Vector3.right * i) + (Vector3.forward * j) + pieceOffSet;

                        InGameHighLiteMove.Add(light);
                    }
                }
            }
        }

        public void HideHightLight() {

            foreach (GameObject g in InGameHighLiteMove) {

                Destroy(g.gameObject);
            }
            InGameHighLiteMove.Clear();
        }

        public void HideCaptureHightLight()
        {

            foreach (GameObject g in InGameHighLiteCapture)
            {
                Destroy(g.gameObject);
            }
            InGameHighLiteCapture.Clear();
        }

        public void CaptureHightLight(bool[,] lightTiles)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (lightTiles[i, j])
                    {
                        var light = Instantiate(CapturePrefabs, transform);

                        light.transform.position = (Vector3.right * i) + (Vector3.forward * j) + pieceOffSet;

                        InGameHighLiteCapture.Add(light);
                    }
                }
            }
        }
    }
}
