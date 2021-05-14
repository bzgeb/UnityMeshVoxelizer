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
                VoxelizeUtils.VoxelizeMesh(meshFilter);
            }
        }
    }

    void OnSceneGUI()
    {
        VoxelizedMesh voxelizedMesh = target as VoxelizedMesh;

        Handles.color = Color.green;
        float size = voxelizedMesh.HalfSize * 2f;

        foreach (Vector3Int gridPoint in voxelizedMesh.GridPoints)
        {
            Vector3 worldPos = voxelizedMesh.PointToPosition(gridPoint);
            Handles.DrawWireCube(worldPos, new Vector3(size, size, size));
        }

        Handles.color = Color.red;
        if (voxelizedMesh.TryGetComponent(out MeshCollider meshCollider))
        {
            Bounds bounds = meshCollider.bounds;
            Handles.DrawWireCube(bounds.center, bounds.extents * 2);
        }
    }
}