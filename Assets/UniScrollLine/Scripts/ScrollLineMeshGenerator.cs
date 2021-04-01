using UnityEngine;

public static class ScrollLineMeshGenerator
{
    public static Mesh Generate(Vector3[] points, Vector3 normal, float width)
    {
        var halfWidth = width / 2f;
        var pointCount = points.Length;
        var vertexCount = pointCount * 2;
        var vertices = new Vector3[vertexCount];
        var uvs = new Vector2[vertexCount];
        var triangles = new int[(pointCount - 1) * 6];
        var uvX = 0f;
        for (var i = 0; i < pointCount; i++)
        {
            Vector3 direction;
            if (i == 0)
            {
                direction = points[1] - points[0];
            }
            else if (i == pointCount - 1)
            {
                direction = points[i] - points[i - 1];
            }
            else
            {
                direction = points[i + 1] - points[i - 1];
            }

            var point = points[i];
            var cross = Vector3.Cross(direction, normal).normalized;
            var vertIndex = i * 2;
            vertices[vertIndex] = point - cross * halfWidth;
            vertices[vertIndex + 1] = point + cross * halfWidth;

            if (i > 0)
            {
                uvX += Vector3.Distance(point, points[i - 1]);
            }

            uvs[vertIndex] = new Vector2(uvX, 0);
            uvs[vertIndex + 1] = new Vector2(uvX, 1);

            if (i == pointCount - 1)
            {
                continue;
            }

            var trisIndex = i * 6;
            triangles[trisIndex] = vertIndex;
            triangles[trisIndex + 1] = vertIndex + 1;
            triangles[trisIndex + 2] = vertIndex + 3;
            triangles[trisIndex + 3] = vertIndex;
            triangles[trisIndex + 4] = vertIndex + 3;
            triangles[trisIndex + 5] = vertIndex + 2;
        }

        var mesh = new Mesh {vertices = vertices, triangles = triangles, uv = uvs};
        return mesh;
    }
}
