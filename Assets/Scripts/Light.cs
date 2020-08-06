using UnityEngine;
[ExecuteAlways]
public class Light : MonoBehaviour
{
    public float viewDistance = 10;
    public LayerMask ignoreLayers;
    public float fov = 360f;
    public int rayCount = 720;
    public float angleOffset = 0;
    private Mesh mesh;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
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
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            
            var hit = Physics2D.Raycast(this.transform.position + origin, Utils.GetVectorFromAngle(angle), viewDistance, ~ignoreLayers);
            //Debug.DrawRay(origin, Utils.GetVectorFromAngle(angle) * viewDistance, Color.red);
            if (hit.collider == null)
            {
                vertex = origin + Utils.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
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

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
