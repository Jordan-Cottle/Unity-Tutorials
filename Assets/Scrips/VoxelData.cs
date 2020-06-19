using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{
    private static readonly int[,,] data = new int[,,] {
        {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {0, 0, 0, 0, 0},
            {0, 1, 1, 1, 0},
            {0, 1, 1, 1, 0},
            {0, 1, 1, 1, 0},
            {0, 0, 0, 0, 0},
        },
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 1, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
        },
    };

    public static int Width{
        get { return data.GetLength(2); }
    }
    public static int Depth{
        get { return data.GetLength(1); }
    }

    public static int Height{
        get { return data.GetLength(0); }
    }

    public static int GetCell(int x, int y, int z){
        return data[y, z, x];
    }

    public static int GetCell(DataCoordinate position){
        return GetCell(position.x, position.y, position.z);
    }

    public static int GetNeighbor (int x, int y, int z, Direction dir){
        DataCoordinate offset = offsets[(int) dir];
        DataCoordinate neighbor = new DataCoordinate(x+offset.x, y+offset.y, z+offset.z);

        Debug.Log($"From <{x},{y},{z}> to <{neighbor.x},{neighbor.y},{neighbor.z}>");
        if (
            neighbor.x < 0
            || neighbor.x >= Width
            || neighbor.y < 0
            || neighbor.y >= Height
            || neighbor.z < 0
            || neighbor.z >= Depth
        ){
            Debug.Log("Out of bounds!");
            return 0;
        }

        Debug.Log($"Looking for cell at [{x},{y},{z}]");
        return GetCell(neighbor.x, neighbor.y, neighbor.z);
    }
    public static int GetNeighbor (Vector3 position, Direction dir){
        return GetNeighbor((int) position.x, (int) position.y, (int) position.z, dir);
    }


    private static DataCoordinate[] offsets = {
        new DataCoordinate(0, 1, 0), // Up
        new DataCoordinate(0, 0, -1), // South
        new DataCoordinate(0, 0, 1), // North
        new DataCoordinate(-1, 0, 0), // West
        new DataCoordinate(1, 0, 0), // East
        new DataCoordinate(0, -1, 0), // Down
    };
}

public struct DataCoordinate{
    public int x;
    public int y;
    public int z;

    public DataCoordinate(int x, int y, int z){
        this.x = x;
        this.y = y;
        this.z = z;
    }
}


public enum Direction{
    Up,
    South,
    North,
    West,
    East,
    Down,
}
