using System.Collections.Generic;
using UnityEngine;

public class VoxelizedMesh : MonoBehaviour
{
    public List<Vector3Int> GridPoints = new List<Vector3Int>();
    public float HalfSize = 0.1f;
    public Vector3 LocalOrigin;

    public Vector3 PointToPosition(Vector3Int point)
    {
        float size = HalfSize * 2f;
        Vector3 pos = new Vector3(HalfSize + point.x * size, HalfSize + point.y * size, HalfSize + point.z * size);
        return LocalOrigin + transform.TransformPoint(pos);
    }
}