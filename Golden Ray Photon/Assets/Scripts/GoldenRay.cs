using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Golden Ray to increase score and damage enemies
/// </summary>
public class GoldenRay : MonoBehaviour
{
    LineRenderer rend;
    EdgeCollider2D col;

    public List<Vector2> linePoints = new List<Vector2>();

    /// <summary>
    /// Called at start of game
    /// </summary>
    private void Start()
    {
        rend = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
    }

    /// <summary>
    /// Called at every frame of game
    /// </summary>
    private void Update()
    {
        linePoints[0] = rend.GetPosition(0);
        linePoints[1] = rend.GetPosition(1);

        col.SetPoints(linePoints);
    }
}
