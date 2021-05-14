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
        //foreach (Vector3 position in voxelizedMesh.Positions)
        foreach (int ind in voxelizedMesh.i)
        {
            Vector3 worldPos = toPos(ind, voxelizedMesh.xMax, voxelizedMesh.yMax, voxelizedMesh.HalfSize,
                voxelizedMesh.minExtents);
            Vector3 localPos = voxelizedMesh.transform.TransformPoint(worldPos);
            Handles.DrawWireCube(localPos, new Vector3(size, size, size));
        }

        if (voxelizedMesh.TryGetComponent(out MeshCollider meshCollider))
        {
            Bounds bounds = meshCollider.bounds;
            Handles.DrawWireCube(bounds.center, bounds.extents * 2);
        }
    }
    
    public Vector3 toPos(int idx, int xMax, int yMax, float halfSize, Vector3 minExtents)
    {
        float size = halfSize * 2;
        int z = idx / (xMax * yMax);
        idx -= (z * xMax * yMax);
        int y = idx / xMax;
        int x = idx % xMax;
        
        Vector3 pos = minExtents + new Vector3(halfSize + x * size, halfSize + y * size, halfSize + z * size);
        return pos;
    }
}