using Assets.Scripts.Checkers;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class CoolDownUtil 
    {
        public static void StartCoolDownPlayer(PlayerType type) {

            TurnCoolDownBehavior[] coolDownPlayers = GameObject.FindObjectsOfType<TurnCoolDownBehavior>();
            if (type.Equals(PlayerType.P1)) {

                foreach(TurnCoolDownBehavior cool in coolDownPlayers) {

                    if (cool.CoolDownPlayerType.Equals(PlayerType.P1)) {
                        cool.CountDownBegin();
                    }
                }
            }

            if (type.Equals(PlayerType.P2))
            {
                foreach (TurnCoolDownBehavior cool in coolDownPlayers)
                {

                    if (cool.CoolDownPlayerType.Equals(PlayerType.P2))
                    {
                        cool.CountDownBegin();
                    }
                }
            }

        }
        public static void ResetCoolDownPlayer(PlayerType type)
        {
            TurnCoolDownBehavior[] coolDownPlayers = GameObject.FindObjectsOfType<TurnCoolDownBehavior>();
            if (type.Equals(PlayerType.P1))
            {
                foreach (TurnCoolDownBehavior cool in coolDownPlayers)
                {
                    if (cool.CoolDownPlayerType.Equals(PlayerType.P1))
                    {
                        cool.CountDownReset();
                    }
                }
            }

            if (type.Equals(PlayerType.P2))
            {
                foreach (TurnCoolDownBehavior cool in coolDownPlayers)
                {

                    if (cool.CoolDownPlayerType.Equals(PlayerType.P2))
                    {
                        cool.CountDownReset();
                    }
                }
            }
        }

    }
}