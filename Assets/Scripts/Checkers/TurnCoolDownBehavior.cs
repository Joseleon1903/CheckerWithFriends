using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Checkers
{
    public class TurnCoolDownBehavior : MonoBehaviour
    {

        private Image countDownImage;

        [Header("Turn Duration Time in Seconds")]
        [SerializeField] private float coolDownDuration;

        [Header("CoolDown player")]
        [SerializeField] private PlayerType _player;

        public PlayerType CoolDownPlayerType { get { return _player; } }

        public bool coolingDown;

        private void Awake()
        {
            countDownImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (coolingDown) {
                //Reduce fill amount over 30 seconds
                countDownImage.fillAmount += 1.0f / coolDownDuration * Time.deltaTime;

                if (countDownImage.fillAmount == 1.0f && 
                    CheckerGameManager.Instance.Player.Type.Equals(_player)) {

                    Debug.Log("Time Out Game Over");
                    if (_player.Equals(PlayerType.P1))
                    {
                        CheckerGameManager.Instance.GameState.OutOfTime(PlayerType.P2);
                    }
                    else 
                    {
                        CheckerGameManager.Instance.GameState.OutOfTime(PlayerType.P1);
                    }
                    
                    CheckerGameManager.Instance.SwitchPlayer();
                    CountDownReset();
                }

            }
        }


        public void CountDownBegin() {
            coolingDown = true;
            countDownImage.fillAmount = 0f;
        }

        public void CountDownReset()
        {
            Debug.Log("Reset player turn CountDown Start");
            coolingDown = false;
            countDownImage.fillAmount = 0f;
        }

    }
}