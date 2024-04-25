using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f;
        viewDistance = 10f;
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        int rayCount = 80;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            float angleRad = angle * (Mathf.PI / 180f);
            Vector3 temp = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, temp, viewDistance, layerMask);
            vertex = origin + temp * viewDistance;
            if (raycastHit2D.collider == null)
            {
                vertex = origin + temp * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 direction)
    {
        //direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        n += 90;
        if (n < 0) n += 360;

       startingAngle = (n - fov / 2f);
    }

    public void SetFov(float fov)
    {
        this.fov = fov;
    }

    public void SetviewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
