using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratedRoom : MonoBehaviour
{
    [SerializeField] private RoomPart[] rooms;
    private List<RoomPart> roomsL = new List<RoomPart>();
    private List<RoomPart> roomsR = new List<RoomPart>();
    private List<RoomPart> roomsU = new List<RoomPart>();
    private List<RoomPart> roomsD = new List<RoomPart>();

    List<RoomConnection> connections = new List<RoomConnection>();

    [SerializeField] private int xSize = 60;
    [SerializeField] private int ySize = 60;
    [SerializeField] private int maxRooms = 7;

    private int[,] tiles;

    [SerializeField] private Tile blackTile;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap ground;

    [SerializeField] private GameObject doorEnterPrefab;
    private GameObject doorEnter;
    private GameObject doorExit;

    private List<Vector2> doorEnterPosiblePositions = new List<Vector2>();
    [SerializeField] RoomPart startingRoom;
    private Room roomObject;
    private AddRoom addRoom;
    private List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    private List<RoomPart> spawnedRooms = new List<RoomPart>();


    public void GenerateRoom()
    {
        tiles = new int[xSize, ySize];
        roomObject = GetComponent<Room>();
        addRoom = GetComponent<AddRoom>();
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
        for (int i = 0; i < maxRooms; i++)
        {
            int connectionsCount = connections.Count;
            if(connections.Count != 0)
            {
                PlaceRoom();
            }
            else
            {
                break;
            }
            if (connections.Count < connectionsCount)
            {
                i--;
            }
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
        roomObject.doorExit = doorExit.GetComponent<Door>();
        doorEnter = Instantiate(doorEnterPrefab, doorEnterPosiblePositions[Random.Range(0, doorEnterPosiblePositions.Count)], Quaternion.identity);
        roomObject.doorEnter = doorEnter.GetComponent<Door>();
        addRoom.doors.Add(doorEnter.gameObject.GetComponent<Collider2D>());
        addRoom.doors.Add(doorExit.gameObject.GetComponent<Collider2D>());

        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            addRoom.enemySpawners.Add(enemySpawner);
        }
        foreach (RoomPart spawnedRoom in spawnedRooms)
        {
            if (spawnedRoom.connections.Count > 0)
                foreach (RoomConnection connection in spawnedRoom.connections)
                {
                    if (connection.door.enabled == false)
                    {
                        Destroy(connection.door);
                        Destroy(connection);
                        continue;
                    }

                connection.door.CompressBounds();
                BoundsInt groundTiles = spawnedRoom.ground.cellBounds;
                Matrix4x4 matr = new Matrix4x4();
                TileBase[] groundBase = connection.door.GetTilesBlock(groundTiles);

                
                for (int i = 0; i < groundTiles.size.x; i++)
                {
                    for (int j = 0; j < groundTiles.size.y; j++)
                    {
                        TileBase groundTile = groundBase[i + j * groundTiles.size.x];
                        if (groundTile != null)
                        {
                            Vector2Int connectionPos = Vector2Int.RoundToInt(new Vector2(
                                spawnedRoom.transform.position.x - transform.position.x,
                                spawnedRoom.transform.position.y - transform.position.y));

                            matr = connection.door.GetTransformMatrix(new Vector3Int(i , j));
                            ground.SetTile(new Vector3Int(i + connectionPos.x, j + connectionPos.y), groundTile);
                            ground.SetTransformMatrix(new Vector3Int(i + connectionPos.x, j + connectionPos.y), matr);

                        }
                    }
                }
                Destroy(connection.gameObject);
            }
        }
    }

    private void PlaceFirstRoom()
    {
        RoomPart firstRoom = startingRoom;
        Vector2Int firstRoomPos = new Vector2Int(xSize / 2 - firstRoom.xSize / 2 + Random.Range(-xSize / 3, xSize / 3), ySize / 2 - firstRoom.ySize / 2 + Random.Range(-ySize / 3, ySize / 3));
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
        doorExit = placedRoom.doorExit;

        placedRoom.ground.CompressBounds();
        BoundsInt groundTiles = placedRoom.ground.cellBounds;
        Matrix4x4 matr = new Matrix4x4();
        TileBase[] groundBase = placedRoom.ground.GetTilesBlock(groundTiles);

        for (int i = 0; i < groundTiles.size.x; i++)
        {
            for (int j = 0; j < groundTiles.size.y; j++)
            {
                TileBase groundTile = groundBase[i + j * groundTiles.size.x];
                if (groundTile != null)
                {
                    matr = placedRoom.ground.GetTransformMatrix(new Vector3Int(i, j));
                    ground.SetTile(new Vector3Int(firstRoomPos.x + i, firstRoomPos.y + j), groundTile);
                    ground.SetTransformMatrix(new Vector3Int(firstRoomPos.x + i, firstRoomPos.y + j), matr);

                }
            }
        }
        Destroy(placedRoom.ground.gameObject);
        //foreach (RoomConnection connection in placedRoom.connections)
        //{
        //    connection.door.CompressBounds();
        //    BoundsInt cGroundTiles = connection.door.cellBounds;
        //    Matrix4x4 cMatr = new Matrix4x4();
        //    TileBase[] cGroundBase = connection.door.GetTilesBlock(cGroundTiles);



        //    for (int i = 0; i < cGroundTiles.size.x; i++)
        //    {
        //        for (int j = 0; j < cGroundTiles.size.y; j++)
        //        {
        //            TileBase groundTile = cGroundBase[i + j * cGroundTiles.size.x];
        //            if (groundTile != null)
        //            {
        //                cMatr = connection.door.GetTransformMatrix(new Vector3Int(i, j));
        //                ground.SetTile(new Vector3Int(firstRoomPos.x + i, firstRoomPos.y + j), groundTile);
        //                ground.SetTransformMatrix(new Vector3Int(firstRoomPos.x + i, firstRoomPos.y + j), cMatr);

        //            }
        //        }
        //    }
        //    Destroy(connection.gameObject);
        //}
            spawnedRooms.Add(placedRoom);
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
                        Destroy(thisRoomConnection.door.gameObject);
                        //thisRoomConnection.door = null;
                        thisRoomConnection.gameObject.SetActive(false);
                    }
                }
                connections.Remove(roomConnection);
                Destroy(roomConnection.door.gameObject);
                //roomConnection.door = null;
                //placedRoom.connections.Remove(roomConnection);
                roomConnection.gameObject.SetActive(false);
                foreach (UnityEngine.Transform doorEnterPosiblePos in placedRoom.doorEnterPosiblePositions)
                {
                    doorEnterPosiblePositions.Add(doorEnterPosiblePos.position);
                }
                foreach(EnemySpawner enemySpawner in placedRoom.EnemySpawners)
                {
                    enemySpawners.Add(enemySpawner);
                }
                placedRoom.ground.CompressBounds();
                BoundsInt groundTiles = placedRoom.ground.cellBounds;
                Matrix4x4 matr = new Matrix4x4();
                TileBase[] groundBase = placedRoom.ground.GetTilesBlock(groundTiles);

                for (int i = 0; i < groundTiles.size.x; i++)
                {
                    for (int j = 0; j < groundTiles.size.y; j++)
                    {
                        TileBase groundTile = groundBase[i + j * groundTiles.size.x];
                        if (groundTile != null)
                        {
                            matr = placedRoom.ground.GetTransformMatrix(new Vector3Int(i, j));
                            ground.SetTile(new Vector3Int(nextRoomPosInt.x + i, nextRoomPosInt.y + j), groundTile);
                            ground.SetTransformMatrix(new Vector3Int(nextRoomPosInt.x + i, nextRoomPosInt.y + j), matr);

                        }
                    }
                }
                Destroy(placedRoom.ground.gameObject);
                spawnedRooms.Add(placedRoom);
                isBreak = true;
            }
        }
        return isBreak;
    }
}