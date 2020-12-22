using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class PongNetworkManager : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    GameObject ball;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 1)
        {
            player.name = "Player1";
        }

        // Name the second player, and spawn ball if two players
        if (numPlayers == 2)
        {
            player.name = "Player2";
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            NetworkServer.Spawn(ball);
        }
        
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (ball != null)
        {
            NetworkServer.Destroy(ball);
        }

        GameObject.Find("Canvas").GetComponent<UpdateUI>().clearScore();

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
