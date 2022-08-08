using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGroundColor : MonoBehaviour
{
    private Transform edges;
    private Transform background;

    [SerializeField]
    private Color[] colors;

    private void Start()
    {
        edges = transform.GetChild(0);
        background = transform.GetChild(1);

        ChangePlayGroundColor();
    }

    private void ChangePlayGroundColor()
    {
        int rngColor = PickColor();

        background.GetComponent<Renderer>().material.color = colors[rngColor];

        foreach (Transform edge in edges)
        {
            edge.GetComponent<Renderer>().material.color = colors[rngColor];
        }
    }

    private int PickColor()
    {
        return Random.Range(0, colors.Length);
    }
}
