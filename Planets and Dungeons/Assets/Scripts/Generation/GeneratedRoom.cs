using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEditor.Experimental.GraphView;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GeneratedRoom : MonoBehaviour
{
    [SerializeField] private RoomPart[] rooms;
    private List<RoomPart> roomsL = new List<RoomPart>();
    private List<RoomPart> roomsR = new List<RoomPart>();
    private List<RoomPart> roomsU = new List<RoomPart>();
    private List<RoomPart> roomsD = new List<RoomPart>();

    List<RoomConnection> connections = new List<RoomConnection>();

    [SerializeField] private static int xSize = 150;
    [SerializeField] private static int ySize = 150;

    private int[,] tiles = new int[xSize, ySize];

    [SerializeField] private Tile blackTile;
    [SerializeField] Tilemap tilemap;


    private void Start()
    {
        foreach (RoomPart room in rooms)
        {
            room.DetermineDirections();
            if (room.hasLeftConnections)
            {
                roomsL.Add(room);
            }
            if (room.hasRightConnections)
            {
                roomsR.Add(room);
            }
            if (room.hasUpConnections)
            {
                roomsU.Add(room);
            }
            if (room.hasDownConnections)
            {
                roomsD.Add(room);
            }
        }
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                tiles[i, j] = 0;
            }
        }
        PlaceFirstRoom();
        for (int i = 0; i < 500; i++)
        {
            PlaceRoom();
        }
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (tiles[i, j] == 0)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), blackTile);
                }
            }
        }
    }

    private void PlaceFirstRoom()
    {
        RoomPart firstRoom = rooms[Random.Range(0, rooms.Length)];
        Vector2Int firstRoomPos = new Vector2Int(Random.Range(0, xSize - firstRoom.xSize), Random.Range(0, ySize - firstRoom.ySize));
        RoomPart placedRoom = Instantiate(firstRoom, transform.position + new Vector3Int(firstRoomPos.x, firstRoomPos.y, 0), Quaternion.identity);
        for (int i = firstRoomPos.x; i < firstRoomPos.x + firstRoom.xSize; i++)
        {
            for (int j = firstRoomPos.y; j < firstRoomPos.y + firstRoom.ySize; j++)
            {
                tiles[i, j] = 1;
            }
        }
        foreach (RoomConnection connection in placedRoom.connections)
        {
            connections.Add(connection);
        }
    }
    private void PlaceRoom()
    {
        RoomConnection roomConnection = connections[Random.Range(0, connections.Count)];
        RoomPart nextRoom;
        bool isBreak = false;
        if (roomConnection.connectionType == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                nextRoom = roomsR[Random.Range(0, roomsR.Count)];
                foreach (RoomConnection connection in nextRoom.connections)
                {
                    if (connection.connectionType == 2)
                    {
                        isBreak = RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        if(isBreak)
                        {
                            break;
                        }
                    }
                }
                if (isBreak)
                {
                    break;
                }
            }
            if (!isBreak)
            {
                connections.Remove(roomConnection);
            }
        }
        else if (roomConnection.connectionType == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                nextRoom = roomsL[Random.Range(0, roomsL.Count)];
                foreach (RoomConnection connection in nextRoom.connections)
                {
                    if (connection.connectionType == 1)
                    {
                        RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        isBreak = RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        if (isBreak)
                        {
                            break;
                        }
                    }
                }
                if (isBreak)
                {
                    break;
                }
            }
            if (!isBreak)
            {
                connections.Remove(roomConnection);
            }
        }
        else if (roomConnection.connectionType == 3)
        {
            for (int i = 0; i < 10; i++)
            {
                nextRoom = roomsD[Random.Range(0, roomsD.Count)];
                foreach (RoomConnection connection in nextRoom.connections)
                {
                    if (connection.connectionType == 4)
                    {
                        RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        isBreak = RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        if (isBreak)
                        {
                            break;
                        }
                    }
                }
                if (isBreak)
                {
                    break;
                }
            }
            if (!isBreak)
            {
                connections.Remove(roomConnection);
            }
        }
        else if (roomConnection.connectionType == 4)
        {
            for (int i = 0; i < 10; i++)
            {
                nextRoom = roomsU[Random.Range(0, roomsU.Count)];
                foreach (RoomConnection connection in nextRoom.connections)
                {
                    if (connection.connectionType == 3)
                    {
                        RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        isBreak = RoomInstantiate(roomConnection, connection, nextRoom, isBreak);
                        if (isBreak)
                        {
                            break;
                        }
                    }
                }
                if (isBreak)
                {
                    break;
                }
            }
            if (!isBreak)
            {
                connections.Remove(roomConnection);
            }
        }
    }
    private bool RoomInstantiate(RoomConnection roomConnection, RoomConnection connection, RoomPart nextRoom, bool isBreak)
    {
        if (roomConnection.transform.position.x - connection.transform.position.x >= transform.position.x &&
                            roomConnection.transform.position.y - connection.transform.position.y >= transform.position.y &&
                            roomConnection.transform.position.x - connection.transform.position.x + nextRoom.xSize < transform.position.x + xSize &&
                            roomConnection.transform.position.y - connection.transform.position.y + nextRoom.ySize < transform.position.y + ySize)
        {
            bool isPossible = true;
            Vector2 nextRoomPos = new Vector2(roomConnection.transform.position.x - connection.transform.position.x - transform.position.x, roomConnection.transform.position.y - connection.transform.position.y - transform.position.y);
            Vector2Int nextRoomPosInt = Vector2Int.RoundToInt(nextRoomPos);
            for (int k = nextRoomPosInt.x; k < nextRoom.xSize + nextRoomPosInt.x; k++)
            {
                for (int j = nextRoomPosInt.y; j < nextRoom.ySize + nextRoomPosInt.y; j++)
                {
                    if (tiles[k ,j] == 1)
                    {
                        isPossible = false;
                    }
                }
            }
            if (isPossible)
            {
                
                RoomPart placedRoom = Instantiate(nextRoom, transform.position + new Vector3Int(nextRoomPosInt.x, nextRoomPosInt.y, 0), Quaternion.identity);
                for (int k = nextRoomPosInt.x; k < nextRoom.xSize + nextRoomPosInt.x; k++)
                {
                    for (int j = nextRoomPosInt.y; j < nextRoom.ySize + nextRoomPosInt.y; j++)
                    {
                        tiles[k, j] = 1;
                    }
                }
                
                foreach (RoomConnection thisRoomConnection in placedRoom.connections)
                {
                    connections.Add(thisRoomConnection);
                    if(thisRoomConnection.transform.position == roomConnection.transform.position)
                    {
                        Debug.Log("Connection destroyed");
                        connections.Remove(thisRoomConnection);
                        Destroy(thisRoomConnection.gameObject);
                    }
                }
                connections.Remove(roomConnection);
                Destroy(roomConnection.gameObject);
                isBreak = true;
            }
        }
        return isBreak;
    }
}