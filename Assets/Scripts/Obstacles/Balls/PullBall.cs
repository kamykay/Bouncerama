using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBall : MonoBehaviour
{
    [Header("Ball Properties")]
    [SerializeField]
    private float magnetForce;

    [SerializeField]
    private float distanceToPull;

    [SerializeField]
    private float distanceToDestroy;

    [Header("Scale Properties")]
    [SerializeField]
    private Vector3 initialScale;

    private Ball ball;

    private void Start()
    {
        ball = transform.GetChild(0).transform.GetComponent<Ball>();

        foreach (Transform child in transform)
        {
            child.localScale = initialScale;
        }
    }

    private void FixedUpdate()
    {
        PullToPlayerBall();
        SetPlayerBallAsTarget();
    }

    private void PullToPlayerBall()
    {
        if (ball.PlayerBall != null)
        {
            foreach (Transform child in transform)
            {
                float distance = Vector3.Distance(child.position, ball.PlayerBall.transform.position);

                if (distance < distanceToPull)
                {
                    child.transform.GetComponent<Ball>().CanFollow = true;   
                }

                if (distance < distanceToDestroy)
                {
                    if (child.transform.GetComponent<Ball>().CanTrigger)
                    {
                        IncreasePlayerBallSize();
                        child.transform.GetComponent<Ball>().CanTrigger = false;
                        ball.PlayerBall.BallCount++;
                    }
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void SetPlayerBallAsTarget()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.GetComponent<Ball>().CanFollow)
            {
                //Vector3 rawDirection = ball.PlayerBall.transform.position - child.transform.position;
                //Vector3 direction = rawDirection.normalized;


                // Rotate the pull ball to face the PC's ball
                child.transform.LookAt(ball.PlayerBall.transform, Vector3.right);

                // Move the pull ball towards the PC's ball
                child.GetComponent<Rigidbody>().MovePosition(Vector3.right * magnetForce);

                
                //child.GetComponent<Rigidbody>().AddRelativeForce(direction * magnetForce, ForceMode.Acceleration);


                //child.GetComponent<Rigidbody>().AddForce(child.transform.position - ball.PlayerBall.transform.position * magnetForce * Time.deltaTime);
                //child.GetComponent<Rigidbody>().MovePosition((direction) * (magnetForce * Time.fixedDeltaTime));
                //child.position = Vector3.MoveTowards(child.position, ball.PlayerBall.transform.position, magnetForce * Time.fixedDeltaTime);
                //child.GetComponent<Rigidbody>().MovePosition(Vector3.SmoothDamp(child.position, ball.PlayerBall.transform.position, ref velocity, step));
            }
        }
    }



    private void IncreasePlayerBallSize()
    {
        ball.PlayerBall.transform.localScale += new Vector3(ball.PlayerBall.AmountToIncreaseScale, ball.PlayerBall.AmountToIncreaseScale, ball.PlayerBall.AmountToIncreaseScale);
    }
}
