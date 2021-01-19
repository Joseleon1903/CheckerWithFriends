using Assets.Scripts.WebSocket.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class HostMatchJoinBehavour: MonoBehaviour
{

    [SerializeField] private Text PlayerOneNameField;

    [SerializeField] private Text PlayerOneIdField;

    [SerializeField] private Text PlayerOneNatioNalityField;

    [SerializeField] private Text PlayerTwoNameField;

    [SerializeField] private Text PlayerTwoIdField;

    [SerializeField] private Text PlayerTwoNationalityField;

    public void SetUpView(PlayerInfo playerOne, PlayerInfo playerTwo) {

        //player One 
        PlayerOneNameField.text = playerOne.name;
        PlayerOneIdField.text = playerOne.playerId;
        PlayerOneNatioNalityField.text = playerOne.nationality;

        //player Two
        PlayerTwoNameField.text = playerTwo.name;
        PlayerTwoIdField.text = playerTwo.playerId;
        PlayerTwoNationalityField.text = playerTwo.nationality;
    }

}
