using System.Linq;
using UnityEditor;
using UnityEngine;

public static class VoxelizeUtils
{
    [MenuItem("Tools/Voxelize Selection")]
    public static void VoxelizeSelectedObject(MenuCommand command)
    {
        GameObject meshFilterGameObject =
            Selection.gameObjects.First(o => o.TryGetComponent(out MeshFilter meshFilter));
        VoxelizeMesh(meshFilterGameObject.GetComponent<MeshFilter>());
    }

    public static void VoxelizeMesh(MeshFilter meshFilter)
    {
        if (!meshFilter.TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider = meshFilter.gameObject.AddComponent<MeshCollider>();
        }

        if (!meshFilter.TryGetComponent(out VoxelizedMesh voxelizedMesh))
        {
            voxelizedMesh = meshFilter.gameObject.AddComponent<VoxelizedMesh>();
        }

        Bounds bounds = meshCollider.bounds;
        Vector3 minExtents = bounds.center - bounds.extents;
        float halfSize = voxelizedMesh.HalfSize;
        Vector3 count = bounds.extents / halfSize;

        int xMax = Mathf.CeilToInt(count.x);
        int yMax = Mathf.CeilToInt(count.y);
        int zMax = Mathf.CeilToInt(count.z);

        voxelizedMesh.GridPoints.Clear();
        voxelizedMesh.LocalOrigin = voxelizedMesh.transform.InverseTransformPoint(minExtents);

        for (int x = 0; x < xMax; ++x)
        {
            for (int z = 0; z < zMax; ++z)
            {
                for (int y = 0; y < yMax; ++y)
                {
                    Vector3 pos = voxelizedMesh.PointToPosition(new Vector3Int(x, y, z));
                    if (Physics.CheckBox(pos, new Vector3(halfSize, halfSize, halfSize)))
                    {
                        voxelizedMesh.GridPoints.Add(new Vector3Int(x, y, z));
                    }
                }
            }
        }
    }
}