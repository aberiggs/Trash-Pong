using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float launchSpeed;
    public float maxSpeedY;
    public Rigidbody2D ballRigidbody;
    public Vector3 startPosition;

    private float speedY;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        speedY = ballRigidbody.velocity.y;
        if (speedY >= maxSpeedY)
        {
            speedY = maxSpeedY;
            ballRigidbody.velocity = new Vector2(ballRigidbody.velocity.x, speedY);
        }
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        ballRigidbody.velocity = new Vector2(launchSpeed * x, launchSpeed * y);
    }

    public void Reset()
    {
        ballRigidbody.velocity = Vector2.zero;
        transform.position = startPosition;
        Launch();
    }
}
