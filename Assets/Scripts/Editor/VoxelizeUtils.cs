using System.Linq;
using UnityEditor;
using UnityEngine;

public static class VoxelizeUtils
{
    [MenuItem("Tools/Voxelize Selection")]
    public static void VoxelizeSelectedObject(MenuCommand command)
    {
        GameObject meshFilterGameObject = Selection.gameObjects.First(o => o.TryGetComponent(out MeshFilter meshFilter));
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
        float size = halfSize * 2f;
        Vector3 count = bounds.extents / halfSize;

        //voxelizedMesh.Positions.Clear();
        voxelizedMesh.i.Clear();

        int xMax = (int)count.x;
        int yMax = (int)count.y;
        int zMax = (int)count.z;
        
        voxelizedMesh.xMax = xMax;
        voxelizedMesh.yMax = yMax;
        voxelizedMesh.zMax = zMax;
        voxelizedMesh.minExtents = minExtents;

        for (int x = 0; x < xMax; ++x)
        {
            for (int z = 0; z < zMax; ++z)
            {
                for (int y = 0; y < yMax; ++y)
                {
                    Vector3 pos = minExtents + new Vector3(halfSize + x * size, halfSize + y * size, halfSize + z * size);
                    if (Physics.CheckBox(pos, new Vector3(halfSize, halfSize, halfSize)))
                    {
                        int index = getIndex(x, y, z, xMax, yMax);
                        voxelizedMesh.i.Add(index);
                        //voxelizedMesh.Positions.Add(voxelizedMesh.transform.InverseTransformPoint(pos));
                    }
                }
            }
        }
    }

    static int getIndex(int x, int y, int z, int xMax, int yMax)
    {
        return (z * xMax * yMax) + (y * xMax) + x; 
    }
}