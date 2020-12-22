using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayer : NetworkBehaviour
{
    public Rigidbody2D paddleRigidbody;
    public float paddleSpeed;

    private float movement;
    private Vector2 startingPosition;

    public override void OnStartServer()
    {
        startingPosition = new Vector2(paddleRigidbody.position.x, 0);
    }


    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            movement = Input.GetAxisRaw("Vertical");
            paddleRigidbody.velocity = new Vector2(paddleRigidbody.velocity.x, movement * paddleSpeed * Time.fixedDeltaTime);
        }

    }

    //Only ran on server
    [Server]
    private void UpdateClientPos()
    {
        Debug.Log("UpdateClientPos function:" + paddleRigidbody.name + isLocalPlayer + transform.position);
        SyncedPos(transform.position);
    }

    [ClientRpc]
    public void SyncedPos(Vector2 syncedPos)
    {
        transform.position = syncedPos;
        Debug.Log("SyncedPosition function:" + paddleRigidbody.name + isLocalPlayer + syncedPos);
    }

    // Only ran on server
    public void resetPosition()
    {
        Debug.Log("resetPosition function:" + startingPosition + paddleRigidbody.name + isLocalPlayer);
        transform.position = startingPosition;

        UpdateClientPos();
    }
}
