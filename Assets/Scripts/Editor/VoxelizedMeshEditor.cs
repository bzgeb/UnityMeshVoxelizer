using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoxelizedMesh))]
public class VoxelizedMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Voxelize Mesh"))
        {
            var voxelizedMesh = target as VoxelizedMesh;
            if (voxelizedMesh.TryGetComponent(out MeshFilter meshFilter))
            {
                VoxelizeSelection.VoxelizeMesh(meshFilter);
            }
        }
    }
}