using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedRoom : MonoBehaviour
{
    [SerializeField] private static int width;
    [SerializeField] private static int height;
    private int[,] tiles = new int[width, height];
    [SerializeField] private RoomPart[] roomParts;
    [SerializeField] private RoomPart[] LRoomParts;
    [SerializeField] private RoomPart[] RRoomParts;
    [SerializeField] private RoomPart[] URroomParts;
    [SerializeField] private RoomPart[] DRoomParts;

    private void Awake()
    {
        RoomPart FirstRoom = roomParts[Random.Range(0, roomParts.Length)];
        int FirstRoomXpos = Random.Range(0, width + 1 - FirstRoom.width);
        int FirstRoomYpos = Random.Range(0, height + 1 - FirstRoom.height);
        Instantiate(FirstRoom, new Vector2(FirstRoomXpos + transform.position.x, FirstRoomYpos + transform.position.y), Quaternion.identity);
        for (int i = FirstRoomXpos; i < FirstRoomXpos + FirstRoom.width; i++)
        {
            for (int j = FirstRoomYpos; j < FirstRoomYpos + FirstRoom.height; j++)
            {
                tiles[i, j] = 1;
            }
        }
        
    }

    private void PlaceRoom()
    {
        
    }
}
