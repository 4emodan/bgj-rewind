using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmController : MonoBehaviour
{
    public GameObject objects;

    private Vector2 lastMousePosition = Vector2.zero;
    private bool touchStarted = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
            var mousePosition = getMousePosition();
            // Debug.Log(mousePosition);

            var delta = mousePosition - lastMousePosition;
            delta.y = 0;

            // var deltaWorld = Camera.main.ScreenToWorldPoint(delta);
            Debug.Log(delta);

            objects.transform.setX(objects.transform.position.x + delta.x);

            lastMousePosition = mousePosition;
        }
    }

    private Vector2 getMousePosition() {
        return Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition).xy();
    }
}
