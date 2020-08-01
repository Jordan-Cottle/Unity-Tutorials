using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private Building[,] buildings = new Building[100, 100];

    public void AddBuilding(Building building, Vector3 position)
    {
        buildings[(int)position.x, (int)position.z] = Instantiate(building, position, Quaternion.identity);
    }

    public bool BuildingAt(Vector3 position)
    {
        return !(buildings[(int)position.x, (int)position.z] is null);
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3((int)position.x, 0, (int)position.z);
    }
}
