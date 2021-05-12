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
            Handles.DrawWireCube(position, new Vector3(size, size, size));
            //Gizmos.DrawCube(position, new Vector3(size, size, size));
            //Gizmos.DrawWireSphere(position, Radius);
        }
    }
}