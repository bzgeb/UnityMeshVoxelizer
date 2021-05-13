using System.Linq;
using UnityEditor;
using UnityEngine;

public static class VoxelizeUtils
{
    [MenuItem("Tools/Voxelize Selection")]
    public static void VoxelizeSelectedObject(MenuCommand command)
    {
        var meshFilterGameObject = Selection.gameObjects.First(o => o.TryGetComponent(out MeshFilter meshFilter));
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

        var bounds = meshCollider.bounds;
        var minExtents = bounds.center - bounds.extents;
        float radius = voxelizedMesh.Radius;
        float size = radius * 2f;
        var count = (bounds.extents) / radius;

        voxelizedMesh.Positions.Clear();

        for (int i = 0; i < count.x; ++i)
        {
            for (int j = 0; j < count.z; ++j)
            {
                for (int w = 0; w < count.y; ++w)
                {
                    var pos = minExtents + new Vector3(radius + i * size, radius + w * size, radius + j * size);
                    if (Physics.CheckBox(pos, new Vector3(radius, radius, radius)))
                    {
                        voxelizedMesh.Positions.Add(voxelizedMesh.transform.InverseTransformPoint(pos));
                    }
                }
            }
        }
    }
}