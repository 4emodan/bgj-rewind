using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowCaster : MonoBehaviour
{

    private Vector2[] currentPath;
    private PolygonCollider2D polygonCollider;
    private Vector2[] idleColliderPath;
    private Vector2[] runningColliderPath;

    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();

        idleColliderPath = polygonCollider.GetPath(0);
        runningColliderPath = new Vector2[idleColliderPath.Length];

        runningColliderPath[0] = new Vector2(0.72f, 1.05f);
        runningColliderPath[1] = new Vector2(1.16f, 1.80f);
        runningColliderPath[2] = new Vector2(1.79f, 1.99f);
        runningColliderPath[3] = new Vector2(0.63f, 2.11f);
        runningColliderPath[4] = new Vector2(-0.20f, 1.43f);
        runningColliderPath[5] = new Vector2(0.62f, 1.67f);
        runningColliderPath[6] = new Vector2(0.60f, 1.08f);
        runningColliderPath[7] = new Vector2(-1.24f, -0.92f);
        runningColliderPath[8] = new Vector2(0.31f, -0.92f);

        currentPath = idleColliderPath;
    }

    public void OnMove() {
        polygonCollider.SetPath(0, runningColliderPath);
    }

    public void OnStop() {
        polygonCollider.SetPath(0, idleColliderPath);
    }
}
