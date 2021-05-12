using System.Linq;
using UnityEditor;
using UnityEngine;

public class VoxelizeSelection : MonoBehaviour
{
    [MenuItem("Tools/Voxelize Selection")]
    public static void VoxelizeMesh(MenuCommand command)
    {
        var meshFilterGameObject = Selection.gameObjects.First(o => o.TryGetComponent(out MeshFilter meshFilter));
        if (!meshFilterGameObject.TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider = meshFilterGameObject.AddComponent<MeshCollider>();
        }

        if (!meshFilterGameObject.TryGetComponent(out VoxelizedMesh voxelizedMesh))
        {
            voxelizedMesh = meshFilterGameObject.AddComponent<VoxelizedMesh>();
        }

        var bounds = meshCollider.bounds;
        var minExtents = bounds.center - bounds.extents;
        var maxExtents = bounds.center + bounds.extents;
        float radius = voxelizedMesh.Radius;
        float size = radius * 2f;
        var count = (bounds.extents * 2f) / radius;

        voxelizedMesh.Positions.Clear();

        for (int i = 0; i < count.x; ++i)
        {
            for (int j = 0; j < count.z; ++j)
            {
                for (int w = 0; w < count.y; ++w)
                {
                    var pos = minExtents + new Vector3(i * size, w * size, j * size);
                    if (Physics.CheckBox(pos, new Vector3(radius, radius, radius)))
                    {
                        voxelizedMesh.Positions.Add(pos);
                    }
                }
            }
        }
    }
}