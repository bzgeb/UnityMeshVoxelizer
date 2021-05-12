using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;

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

        var bounds = meshCollider.bounds;
        var minExtents = bounds.center - bounds.extents;
        var maxExtents = bounds.center + bounds.extents;
        float radius = 0.05f;
        var count = (bounds.extents * 2f) / radius;

        for (int i = 0; i < count.x; ++i)
        {
            for (int j = 0; j < count.z; ++j)
            {
                for (int w = 0; w < count.y; ++w)
                {
                    var pos = minExtents + new Vector3(i * radius, w * radius, j * radius);
                    if (Physics.CheckSphere(pos, radius))
                    {
                        GameObject go = new GameObject("New");
                        go.transform.position = pos;
                    }
                }
            }
        }
    }
}