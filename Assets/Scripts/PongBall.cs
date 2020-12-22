using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : NetworkBehaviour
{
    public float launchSpeed;
    public float maxSpeedY;
    public float ballSendSpeedMultiplier;
    public float maxBounceAngle;
    public Rigidbody2D ballRigidbody;

    private float relativeIntersectY;
    private float paddleHeight;
    private float paddleY;
    private float intersectY;
    private float normalizedRelativeIntersectionY;
    private float bounceAngle;

    public override void OnStartServer()
    {
        base.OnStartServer();

        // only simulate ball physics on server
        ballRigidbody.simulated = true;

        ballSetup();
    }

    public void ballSetup()
    {
        ballRigidbody.velocity = Vector2.zero;
        ballRigidbody.position = Vector2.zero;
        Launch();
    }
   
    private void Launch()
    {
        //Launch the ball in a random direction
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        ballRigidbody.velocity = new Vector2(launchSpeed * x, launchSpeed * y*0);
    }

   
    [ServerCallback]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<PongPlayer>())
        {
            paddleY = collision.transform.position.y;
            intersectY = ballRigidbody.position.y;

            //TODO: remove this hardcoded value
            paddleHeight = 3;

            relativeIntersectY = (paddleY - intersectY);
            normalizedRelativeIntersectionY = (relativeIntersectY / (paddleHeight / 2));
            bounceAngle = normalizedRelativeIntersectionY * maxBounceAngle;

            //TODO: Fix the commented out portion so that racket velocity can affect the y speed of ball.
            if (collision.transform.position.x > 0)
            {
                ballRigidbody.velocity = new Vector2(ballSendSpeedMultiplier * -1, ballSendSpeedMultiplier * -Mathf.Sin(bounceAngle) /*+ paddleRigidbody.velocity.y * paddleVelocityMultiplier*/);
            }
            else
            {
                ballRigidbody.velocity = new Vector2(ballSendSpeedMultiplier * 1, ballSendSpeedMultiplier * -Mathf.Sin(bounceAngle) /*+ paddleRigidbody.velocity.y * paddleVelocityMultiplier*/);
            }
        }
    }
   
}
