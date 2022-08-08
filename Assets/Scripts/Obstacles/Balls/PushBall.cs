using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    [Header("Ball Properties")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float distanceToPush;

    [SerializeField]
    private float distanceToDestroy;

    [Header("Scale Properties")]
    [SerializeField]
    private Vector3 initialScale;

    private BallController playerBall;
    private PaddleController playerPaddle;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.localScale = initialScale;
        }

        playerBall = FindObjectOfType<BallController>();
        playerPaddle = FindObjectOfType<PaddleController>();
    }

    private void Update()
    {
        PullToPlayerBall();
        SetPlayerBallAsTarget();
    }

    private void PullToPlayerBall()
    {
        foreach (Transform child in transform)
        {
            float distance = Vector3.Distance(child.position, playerBall.transform.position);

            if (distance < distanceToPush)
            {
                //child.transform.GetComponent<Ball>().CanFollow = true;
            }

            if (distance < distanceToDestroy)
            {
                if (child.transform.GetComponent<Ball>().CanTrigger)
                {
                    DecreasePlayerBallSize();
                    DecreasePlayerPaddleSize();
                    child.transform.GetComponent<Ball>().CanTrigger = false;
                }
                child.gameObject.SetActive(false);
            }
        }
    }

    private void SetPlayerBallAsTarget()
    {
        float step = speed * Time.deltaTime;

        foreach (Transform child in transform)
        {
            if (child.transform.GetComponent<Ball>().CanFollow)
            {
                child.position = Vector3.MoveTowards(child.position, -playerBall.transform.position, step);
            }
        }
    }

    private void DecreasePlayerBallSize()
    {
        playerBall.transform.localScale -= new Vector3(playerBall.AmountToDecreaseScale, playerBall.AmountToDecreaseScale, playerBall.AmountToDecreaseScale);
    }

    private void DecreasePlayerPaddleSize()
    {
        playerPaddle.transform.localScale -= new Vector3(playerPaddle.AmountToDecreaseScaleX, 0f, 0f);
    }
}
