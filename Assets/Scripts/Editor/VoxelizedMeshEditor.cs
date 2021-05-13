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
        var voxelizedMesh = target as VoxelizedMesh;

        Handles.color = Color.green;
        float size = voxelizedMesh.Radius * 2f;
        foreach (var position in voxelizedMesh.Positions)
        {
            var localPos = voxelizedMesh.transform.TransformPoint(position);
            Handles.DrawWireCube(localPos, new Vector3(size, size, size));
        }

        if (voxelizedMesh.TryGetComponent(out MeshCollider meshCollider))
        {
            var bounds = meshCollider.bounds;
            Handles.DrawWireCube(bounds.center, bounds.extents * 2);
        }
    }
}