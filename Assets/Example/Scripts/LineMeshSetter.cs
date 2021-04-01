using System.Collections.Generic;
using UnityEngine;

namespace USL.Sample
{
    public class LineMeshSetter : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter = null;

        public void ApplyMesh(List<Vector3> points)
        {
            _meshFilter.sharedMesh = ScrollLineMeshGenerator.Generate(points.ToArray(), Vector3.up, 0.5f);
        }
    }
}