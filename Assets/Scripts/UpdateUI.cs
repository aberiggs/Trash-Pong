using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateUI : NetworkBehaviour
{
    public GameObject player1Text;
    public GameObject player2Text;

    [SyncVar(hook = nameof(UpdatePlayer1ScoreUI))]
    private int player1Score;
    [SyncVar(hook = nameof(UpdatePlayer2ScoreUI))]
    private int player2Score;

    public void playerScores(bool isPlayer1Goal)
    {
        if (!isPlayer1Goal) { player1Score++; }
        else { player2Score++; }
    }

    public void UpdatePlayer1ScoreUI(int oldPlayerScore, int newPlayerScore)
    {
        player1Text.GetComponent<TextMeshProUGUI>().text = newPlayerScore.ToString();
    }

    public void UpdatePlayer2ScoreUI(int oldPlayerScore, int newPlayerScore)
    {
        player2Text.GetComponent<TextMeshProUGUI>().text = newPlayerScore.ToString();
    }

    public void clearScore()
    {
        player1Score = 0;
        player2Score = 0;
    }
}
