using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float paddleSpeed;
    public float ballSendSpeedMultiplier;
    public float maxBounceAngle;
    public float paddleVelocityMultiplier;
    public Rigidbody2D paddleRigidbody;
    public GameObject paddle;
    public Rigidbody2D ballRigidbody;
    public GameObject ball;
    public Vector3 startPosition;
   

    private float movement;


    private float relativeIntersectY;
    private float paddleHeight;
    private float paddleY;
    private float intersectY;
    private float normalizedRelativeIntersectionY;
    private float bounceAngle;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Vertical");
        }

        else
        {
            movement = Input.GetAxisRaw("Vertical2");
        }

        paddleRigidbody.velocity = new Vector2(paddleRigidbody.velocity.x, movement * paddleSpeed); 
    }

    public void Reset()
    {
        paddleRigidbody.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayer1)
            {
                paddleY = paddleRigidbody.position.y;
                intersectY = ballRigidbody.position.y;
                paddleHeight = 3;

                relativeIntersectY = (paddleY - intersectY);
                normalizedRelativeIntersectionY = (relativeIntersectY / (paddleHeight / 2));
                bounceAngle = normalizedRelativeIntersectionY * maxBounceAngle;

                ballRigidbody.velocity = new Vector2(ballSendSpeedMultiplier * 1, ballSendSpeedMultiplier * -Mathf.Sin(bounceAngle) + paddleRigidbody.velocity.y * paddleVelocityMultiplier);
            }
            else
            {
                paddleY = paddleRigidbody.position.y;
                intersectY = ballRigidbody.position.y;
                paddleHeight = 3;

                relativeIntersectY = (paddleY - intersectY);
                normalizedRelativeIntersectionY = (relativeIntersectY / (paddleHeight / 2));
                bounceAngle = normalizedRelativeIntersectionY * maxBounceAngle;

                ballRigidbody.velocity = new Vector2((ballSendSpeedMultiplier * -1), ballSendSpeedMultiplier * -Mathf.Sin(bounceAngle) + paddleRigidbody.velocity.y * paddleVelocityMultiplier);
            }
        }
    }
}
