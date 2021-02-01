using Assets.Scripts.WebSocket;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rail rail;

    public PlayMode mode;

    public float speed = 2.5f;
    public bool isReversed;
    public bool isLooping;
    public bool isPingPong;

    private int currentSeg;
    private float transition;
    private bool isComplete;

    [SerializeField] private GameObject WhitePlayerRail;

    [SerializeField] private GameObject BlackPlayerRail;

    private void Start()
    {
        var client = FindObjectOfType<ClientWSBehavour>();
        bool isWhite = (client != null) ? client.profile.isHost : true;
        InstanceRailForPlayer(isWhite);
    }

    private void InstanceRailForPlayer(bool isWhite) {

        if (isWhite) {
            WhitePlayerRail.SetActive(true);
        }
        else {
            BlackPlayerRail.SetActive(true);
        }

        rail = FindObjectOfType<Rail>();
    }

    void Update()
    {

        if (isComplete && CheckerGameManager.Instance.GameState.IsRail)
        {
            CheckerGameManager.Instance.GameState.Start();
            CheckerGameManager.Instance.StartGame();
        }

        if (!rail)
            return;

        if (!isComplete)
            Play(!isReversed);

       
    }

    private void Play(bool foward = true)
    {

        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;

        float s = (Time.deltaTime * 1 / m) * speed;

        transition += (foward) ? s : -s;
         
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            if (currentSeg == rail.nodes.Length -1) 
            {
                if (isLooping) 
                {
                    if (isPingPong) 
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = 0;
                    }
                }
                else{
                    isComplete = true;
                    return;
                }
            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;

            if (currentSeg == -1)
            {
                if (isLooping)
                {
                    if (isPingPong)
                    {
                        transition = 0;
                        currentSeg = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = rail.nodes.Length-2;

                    }
                }
                else
                {
                    isComplete = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}
