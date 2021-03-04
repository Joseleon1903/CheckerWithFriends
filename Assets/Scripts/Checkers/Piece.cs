using Assets.Scripts.Outliner;
using UnityEngine;

namespace Assets.Scripts.Checkers
{
    public struct Position
    {
        public int x;
        public int y;
    }

    public abstract class Piece : MonoBehaviour
    {
        [SerializeField]
        protected bool isWhite;

        [SerializeField]
        protected bool isKing;

        [SerializeField]
        protected GameObject floatingMessage;

        protected Outline outline;

        public bool IsWhite { get { return isWhite; } }

        public bool IsKing { get { return isKing; } }

        private void Start()
        {
            outline = GetComponent<Outline>();
        }

        public virtual void EnableOutline() 
        {
            outline.eraseRenderer = false ;
        }

        public virtual void DisableOutline()
        {
            outline.eraseRenderer = true;
        }

        public virtual void ShowPieceMessage(string messageIn) {

            if (GetComponentInChildren<PieceFloatingText>( true) == null) {
                var message = Instantiate(floatingMessage, transform.position, Quaternion.identity, transform);
                message.GetComponent<TextMesh>().text = messageIn;
            }
        }

        public virtual void ShowPromotedPieceMessage()
        {
            var message = Instantiate(floatingMessage, transform.position, Quaternion.identity, transform);
            message.GetComponent<TextMesh>().color = Color.yellow;
            message.GetComponent<TextMesh>().text = "Queen promoted piece";
        }

    }
}