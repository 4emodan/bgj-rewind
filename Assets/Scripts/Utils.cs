using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}

public static class Components
{
    public static Transform setX(this Transform it, float x)
    {
        it.transform.position = new Vector3(x, it.transform.position.y, it.transform.position.z);
        return it;
    }
}

public static class Vectors {
    public static Vector2 xy(this Vector3 v) {
        return new Vector2(v.x, v.y);
    }
}
