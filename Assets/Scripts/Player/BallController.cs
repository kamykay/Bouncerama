using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball Properties")]
    [SerializeField]
    private float maxVelocity;

    [Space]

    [SerializeField]
    private Vector3 initialScale;

    [SerializeField]
    private Vector3 maxScale;

    [SerializeField]
    private Vector3 minScale;

    [SerializeField]
    private float amountToIncreseScale;

    [SerializeField]
    private float amountToDecreaseScale;

    public int ballCount;

    [Header("Collision layer")]
    [SerializeField]
    private LayerMask mask;

    private Rigidbody rb;
    private PaddleController playerPaddle;

    public float AmountToIncreaseScale
    {
        get { return amountToIncreseScale; }
        set { amountToIncreseScale = value; }
    }

    public float AmountToDecreaseScale
    {
        get { return amountToDecreaseScale; }
        set { amountToDecreaseScale = value; }
    }

    public int BallCount
    {
        get { return ballCount; }
        set { ballCount = value; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerPaddle = FindObjectOfType<PaddleController>();

        ResetScale();

        //Ignore Collisions with ball obstacles
        IgnoreCollisionsWithBallObstacles();
    }

    private void Update()
    {
        MaxScaleSize();
        MinScaleSize();
    }

    private void FixedUpdate()
    {
        LimitMaxVelocity();
    }

    private void IgnoreCollisionsWithBallObstacles() 
    {
        Ball[] ballObstacles = GameObject.FindObjectsOfType<Ball>();

        foreach (Ball ball in ballObstacles)
        {
            Physics.IgnoreCollision(ball.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((mask.value & 1 << collision.gameObject.layer) != 0 << collision.gameObject.layer)
        {
            rb.AddForce(Vector3.up * maxVelocity);
        }

        if (collision.gameObject == playerPaddle.gameObject)
        {
            if (ballCount >= 1)
            {
                for (int i = 0; i <= ballCount; i++)
                {
                    IncreasePlayerPaddleSize();
                }
                ballCount = 0;
                ResetScale();
            }
        }
    }

    private void LimitMaxVelocity()
    {
        float speed = Vector3.Magnitude(rb.velocity);

        if (speed > maxVelocity)
        {
            float brakeSpeed = speed - maxVelocity;

            Vector3 normalizedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalizedVelocity * brakeSpeed;

            rb.AddForce(-brakeVelocity);
        }
    }

    private void MaxScaleSize()
    {
        if (transform.localScale.x >= maxScale.x)
        {
            transform.localScale = maxScale;
        }
    }

    private void MinScaleSize()
    {
        if (transform.localScale.x <= minScale.x)
        {
            transform.localScale = minScale;
        }
    }

    private void ResetScale()
    {
        transform.localScale = initialScale;
    }

    private void IncreasePlayerPaddleSize()
    {
        playerPaddle.transform.localScale += new Vector3(playerPaddle.AmountToIncreseScaleX, 0f, 0f);
    }
}
