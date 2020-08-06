using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class PlayerDetector : MonoBehaviour
{
    public LayerMask ignoreLayers;
    public LayerMask targetLayers;
    public float viewDistance = 5;
    public float fov = 45f;
    public float angleOffset = 0;
    public int rayCount = 50;
    public bool right = true;
    public bool isShowing = true;
    public Transform player;

    private Mesh mesh;
    private bool playerDetected = false;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        var angle = angleOffset;
        var dAngle = fov / rayCount;
        var origin = Vector3.zero;
        var vertices = new Vector3[rayCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[rayCount * 3];

        vertices[0] = origin;

        var vertexIndex = 1;
        var triangleIndex = 0;
        playerDetected = false;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            float resAngle = right ? angle : 180 - angle;
            var hit = Physics2D.Raycast(this.transform.position + origin, Utils.GetVectorFromAngle(resAngle), viewDistance, ~ignoreLayers);
            if (hit.collider == null)
            {
                vertex = origin + Utils.GetVectorFromAngle(resAngle) * viewDistance;
            }
            else
            {
                if ((targetLayers & 1 << hit.collider.gameObject.layer) > 0)
                {
                    playerDetected = true;
                    player = hit.collider.transform;
                }
                vertex = hit.point;
                vertex += -this.transform.position;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            angle -= dAngle;
            vertexIndex += 1;
        }
        if (isShowing)
        {
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        }
        else
        {
            mesh.vertices = new Vector3[3];
            mesh.uv = new Vector2[3];
            mesh.triangles = new int[3];
        }
    }

    public void Flip()
    {
        right = !right;
    }

    public void lookLeft()
    {
        right = false;
    }
    public void lookRight()
    {
        right = true;
    }

    public bool isPlayerDetected()
    {
        return playerDetected;
    }
}
