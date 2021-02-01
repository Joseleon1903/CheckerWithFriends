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

    }
}