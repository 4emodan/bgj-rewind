using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmController : MonoBehaviour
{
    public GameObject objects;
    public float maxDistance = 12f;
    public float revertSpeed = 5f;

    private Vector2 lastMousePosition = Vector2.zero;
    private bool touchStarted = false;
    private Rect filmRect;
    private float originalX;
    private float revertDistance = 0f;

    private void Start()
    {
        var size = transform.localScale.xy();
        filmRect = new Rect(transform.position.xy() - size / 2f, size);
        // Debug.Log($"{name}: {filmRect}");
    }

    void Update()
    {
        var mousePosition = getMousePosition();

        if (Input.GetMouseButtonDown(0) && isPointInside(mousePosition) && revertDistance == 0f)
        {
            touchStarted = true;
            originalX = objects.transform.position.x;
            lastMousePosition = mousePosition;
            Debug.Log($"originalX = {originalX}");
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchStarted = false;
        }
        else if (touchStarted)
        {
            var delta = mousePosition - lastMousePosition;
            delta.y = 0;

            var resultX = objects.transform.position.x + delta.x;
            var msg = $"resultX = {resultX}";
            if (resultX > originalX)
            {
                resultX = originalX;
            }
            else if (originalX - resultX > maxDistance)
            {
                resultX = originalX - maxDistance;
            }
            Debug.Log(msg + $" => {resultX}");
            revertDistance = originalX - resultX;
            objects.transform.setX(resultX);

            lastMousePosition = mousePosition;
        }
        else if (revertDistance > 0f)
        {
            var deltaX = Mathf.Min(revertDistance, Time.deltaTime * revertSpeed);
            revertDistance -= Mathf.Abs(deltaX);
            if (revertDistance < 0f) {
                revertDistance = 0f;
            }

            var resultX = objects.transform.position.x + deltaX;
            if (resultX > originalX) {
                resultX = originalX;
            }
            objects.transform.setX(resultX);
        }
    }

    private bool isPointInside(Vector2 point)
    {
        return filmRect.Contains(point);
    }

    private Vector2 getMousePosition()
    {
        return Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition).xy();
    }
}
