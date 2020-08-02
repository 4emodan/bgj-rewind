using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightEdges : MonoBehaviour
{
    // Start is called before the first frame update
     
    void Start()
    {
        var collider = GetComponent<EdgeCollider2D>();
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(-width / 2, -height / 2);
        points[1] = new Vector2(width / 2, -height / 2);
        points[2] = new Vector2(width / 2, height / 2);
        points[3] = new Vector2(-width / 2, height / 2);
        points[4] = new Vector2(-width / 2, -height / 2);
        collider.points = points;
    }

}
