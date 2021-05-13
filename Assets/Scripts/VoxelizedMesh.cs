using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VoxelizedMesh : MonoBehaviour
{
    public List<Vector3> Positions = new List<Vector3>();
    public float Radius = 0.25f;

    void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Gizmos.color = Color.green;
        float size = Radius * 2f;
        foreach (var position in Positions)
        {
            var localPos = transform.TransformPoint(position);
            Handles.DrawWireCube(localPos, new Vector3(size, size, size));
            //Gizmos.DrawCube(position, new Vector3(size, size, size));
            //Gizmos.DrawWireSphere(position, Radius);
        }

        if (TryGetComponent(out MeshCollider meshCollider))
        {
            var bounds = meshCollider.bounds;
            Handles.DrawWireCube(bounds.center, bounds.extents * 2);
        }
    }
}