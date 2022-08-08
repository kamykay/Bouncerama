using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool canFollow = false;

    private bool canTrigger = true;

    [SerializeField]
    private BallController playerBall;

    private void Start()
    {
        playerBall = FindObjectOfType<BallController>();
    }

    public BallController PlayerBall
    {
        get { return playerBall; }
        set { playerBall = value; }
    }

    public bool CanFollow
    {
        get { return canFollow; }
        set { canFollow = value; }
    }

    public bool CanTrigger
    {
        get { return canTrigger; }
        set { canTrigger = value; }
    }
}
