using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Path path = null;
    public float speed = 1;
    public bool isLerp = false;
    private int currentPoint = 0;
    private int direction = 1;
    private Vector3[] fullPath = null;
    private Vector3 currentPosition;
    private Vector3 delta;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (collider.tag == "Player")
        {
            collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (collider.tag == "Player")
        {
            collider.transform.SetParent(null);
        }
    }

    void Start()
    {
        currentPosition = Vector3.zero;
    }
    void Update()
    {
        if (path == null)
        {
            fullPath = new Vector3[1];
            fullPath[0] = Vector3.zero;
        }
        else
        {
            if (currentPoint == 0 || currentPoint == fullPath.Length - 1)
            {
                fullPath = new Vector3[path.points.Length + 1];
                fullPath[0] = path.start;
                for (int i = 0; i < path.points.Length; i++)
                {
                    fullPath[i + 1] = path.points[i];
                }
            }
        }
        if (path == null)
        {
            return;
        }
        int nextPoint = currentPoint + direction;
        if (nextPoint == -1 || nextPoint == fullPath.Length)
        {
            direction *= -1;
        }
        nextPoint = currentPoint + direction;
        if (Vector3.Magnitude(currentPosition - fullPath[nextPoint]) < 0.1f)
        {
            currentPoint = nextPoint;
        }
        Vector3 directionVec = fullPath[nextPoint] - currentPosition;
        if (isLerp)
        {
            delta = Vector3.Lerp(currentPosition, fullPath[nextPoint], speed / 100) - currentPosition;
        }
        else
        {
            delta = directionVec.normalized * Time.deltaTime * speed / 5;
            if (Vector3.Magnitude(delta) > Vector3.Magnitude(directionVec))
            {
                delta = directionVec;
            }
        }
    }

    void LateUpdate()
    {
        currentPosition += delta;
        this.transform.Translate(delta);
    }
}
