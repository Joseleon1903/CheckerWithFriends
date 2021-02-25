using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Validation
{
    public class CreateLobbyValidation
    {
        public CreateLobbyValidation() { }

        public bool ValidateLobbyEnoughMoney(string bet) {

            Debug.Log("Entering in method ValidateLobbyEnoughMoney ");
            Debug.Log("Bet validation ");

            int betNumbre = int.Parse(bet);

            int profileMoney = int.Parse(Finder.FindGameProfile().GetComponent<BaseProfile>()._playerCoins);

            if (profileMoney < betNumbre) {

                Debug.Log("Player not have money");
                return false;
            }
            Debug.Log("Player have money");
            return true;
        }

    }
}