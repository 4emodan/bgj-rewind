using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmController : MonoBehaviour
{
    public GameObject objects;
    public float maxDistance = 12f;
    public float revertSpeed = 5f;

    private float originalX;
    private float revertDistance = 0f;

    private void Start()
    {
        originalX = objects.transform.position.x;
    }

    void Update()
    {
        if (revertDistance > 0f)
        {
            var deltaX = Mathf.Min(revertDistance, Time.deltaTime * revertSpeed);
            revertDistance -= Mathf.Abs(deltaX);
            if (revertDistance < 0f)
            {
                revertDistance = 0f;
            }

            var resultX = objects.transform.position.x + deltaX;
            if (resultX > originalX)
            {
                resultX = originalX;
            }
            objects.transform.setX(resultX);
        }
    }

    public bool rewind(float delta)
    {
        bool ans = true;
        var resultX = objects.transform.position.x + delta;
        if (resultX > originalX)
        {
            resultX = originalX;
        }
        else if (originalX - resultX > maxDistance)
        {
            resultX = originalX - maxDistance;
            ans = false;
        }
        revertDistance = originalX - resultX;
        objects.transform.setX(resultX);
        return ans;
    }

    public float getRevertDistance()
    {
        return revertDistance;
    }
}
