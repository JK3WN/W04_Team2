using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public float cellSize = 1f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x);
        int yCount = Mathf.RoundToInt(position.y);
        Vector3 result = new Vector3((float)xCount * cellSize + 0.5f, (float)yCount * cellSize + 0.5f, 0f);
        result += transform.position;
        return result;
    }
}
