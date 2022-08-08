using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PaddleController : MonoBehaviour
{
    [Header("Paddle Rotation Properties")]
    [SerializeField]
    private float rotationSpeed = 30f;

    [SerializeField]
    private float angle;

    [Header("Paddle Scale Properties")]
    [SerializeField]
    private float initialScaleX;

    [SerializeField]
    private float maxScaleX;

    [SerializeField]
    private float minScaleX;

    [Space]

    [SerializeField]
    private float amountToIncreseScaleX;

    [SerializeField]
    private float amountToDecreaseScaleX;

    private Transform pivot;

    public float AmountToIncreseScaleX
    {
        get { return amountToIncreseScaleX; }
        set { amountToIncreseScaleX = value; }
    }

    public float AmountToDecreaseScaleX
    {
        get { return amountToDecreaseScaleX; }
        set { amountToDecreaseScaleX = value; }
    }

    private void Start()
    {
        transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        pivot = transform.parent;
    }

    private void Update()
    {
        MovePaddleToTouchInputPosition();

        MaxScaleSize();
        MinScaleSize();

        if (transform.localScale.x >= initialScaleX)
        {
            NormalizedMaxAngle();
        }
        else
        {
            NormalizedMinAngle();
        }
    }

    private void MovePaddleToTouchInputPosition()
    {
        Vector3 localAngle = pivot.transform.localEulerAngles;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            localAngle.z += rotationSpeed * touch.deltaPosition.x;
            pivot.transform.localEulerAngles = new Vector3(0, 0, ClampAngle(localAngle.z, -angle, angle));
        }
    }

    private void NormalizedMaxAngle() 
    {
        float normalizedAngle = Mathf.InverseLerp(initialScaleX, maxScaleX, transform.localScale.x);

        angle = Mathf.Lerp(25, 15, normalizedAngle);
    }

    private void NormalizedMinAngle()
    {
        float normalizedAngle = Mathf.InverseLerp(initialScaleX, minScaleX, transform.localScale.x);

        angle = Mathf.Lerp(25, 35, normalizedAngle);
    }

    private void MaxScaleSize()
    {
        if (transform.localScale.x >= maxScaleX)
        {
            transform.localScale = new Vector3(maxScaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void MinScaleSize()
    {
        if (transform.localScale.x <= minScaleX)
        {
            transform.localScale = new Vector3(minScaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    #region Angle Calculations
    protected float ClampAngle(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        if (angle > 180)
        {
            angle -= 360;
        }
        else if (angle < -180)
        {
            angle += 360;
        }

        min = NormalizeAngle(min);
        if (min > 180)
        {
            min -= 360;
        }
        else if (min < -180)
        {
            min += 360;
        }

        max = NormalizeAngle(max);
        if (max > 180)
        {
            max -= 360;
        }
        else if (max < -180)
        {
            max += 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    protected float NormalizeAngle(float angle)
    {
        while (angle > 360)
        {
            angle -= 360;
        }    
        while (angle < 0)
        {
            angle += 360;
        } 
        return angle;
    }
    #endregion
}