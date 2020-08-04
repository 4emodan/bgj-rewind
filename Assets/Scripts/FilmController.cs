using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmController : MonoBehaviour
{
    public GameObject objects;

    private Vector2 lastMousePosition = Vector2.zero;
    private bool touchStarted = false;
    private Rect filmRect;

    private void Start() {
        var size = transform.localScale.xy();
        filmRect = new Rect(transform.position.xy() - size / 2f, size);
        Debug.Log($"{name}: {filmRect}");
    }

    void Update()
    {
        var mousePosition = getMousePosition();
        
        if (Input.GetMouseButtonDown(0) && isPointInside(mousePosition))
        {
            touchStarted = true;
            lastMousePosition = getMousePosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchStarted = false;
        }

        if (touchStarted)
        {
            var delta = mousePosition - lastMousePosition;
            delta.y = 0;

            objects.transform.setX(objects.transform.position.x + delta.x);

            lastMousePosition = mousePosition;
        }
    }

    private bool isPointInside(Vector2 point) {
        return filmRect.Contains(point);
    }

    private Vector2 getMousePosition() {
        return Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition).xy();
    }
}
