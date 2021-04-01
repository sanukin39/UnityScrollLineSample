using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace USL.Sample
{
    public class InputDetector : MonoBehaviour
    {
        [SerializeField] private LineMeshSetter _meshSetter = null;

        private Camera _camera;
        private List<Vector3> _points = new List<Vector3>();
        private Vector3 _previousPoint;

        void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 100))
                {
                    var point = hit.point;
                    if (_points.Any() && Vector3.Distance(point, _previousPoint) < 0.1f)
                    {
                        return;
                    }

                    _points.Add(point);
                    _previousPoint = point;

                    if (_points.Count > 1)
                    {
                        _meshSetter.ApplyMesh(_points);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _points.Clear();
            }
        }
    }
}