using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomPart : MonoBehaviour
{
    public List<RoomConnection> connections;
    public int xSize;
    public int ySize;

    public Transform[] doorEnterPosiblePositions;
    public GameObject doorExit;
    public Tilemap ground;
    public EnemySpawner[] EnemySpawners;

    public bool hasLeftConnections { get; private set; } = false;
    public bool hasRightConnections { get; private set; } = false;
    public bool hasUpConnections { get; private set; } = false;
    public bool hasDownConnections { get; private set; } = false;
    public void DetermineDirections()
    {
        foreach (var connection in connections)
        {
            if (connection.connectionType == 1)
            {
                hasLeftConnections = true;
            }
            else if (connection.connectionType == 2)
            {
                hasRightConnections = true;
            }
            else if (connection.connectionType == 3)
            {
                hasUpConnections = true;
            }
            else if (connection.connectionType == 4)
            {
                hasDownConnections = true;
            }
        }
    }
}
