using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGoal : NetworkBehaviour
{
    public bool isPlayer1Goal;

    [ServerCallback]
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ball"))
        {
            //Update score
            GameObject.Find("Canvas").GetComponent<UpdateUI>().playerScores(isPlayer1Goal);

            //Reset position of rackets and ball
            //TODO: Remove clone from ball name in PongNetworkManager
            GameObject.Find("Ball(Clone)").GetComponent<PongBall>().ballSetup();
            GameObject.Find("Player1").GetComponent<PongPlayer>().resetPosition();
            GameObject.Find("Player2").GetComponent<PongPlayer>().resetPosition();
        }
    }
}
