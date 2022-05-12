using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Room[] RoomPrefabs;
    public Room startingRoom;
    [SerializeField] private Room finalRoom;
    public int Blocks;
    public int Offset;
    [SerializeField] private PlayerStats ps;
    [SerializeField] private int roomsAmount;

    public List<GameObject> Doors;
    public GameObject[] DoorsOnSpawn;
    private Room[] spawnedRooms;
    private Room newRoom;
    void Start()
    {
        spawnedRooms = new Room[roomsAmount];

        for (int i = 0; i < spawnedRooms.Length; i++)
        {
            if (i == 0)
            {
                newRoom = Instantiate(startingRoom);
            }
            else if (i == spawnedRooms.Length - 1)
            {
                newRoom = Instantiate(finalRoom);
            }
            else
            {
                newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
            }
            newRoom.transform.position = new Vector2(Offset, i * Blocks);
            newRoom.Index = i;
            if(newRoom.TryGetComponent(out AddRoom ar))
            {
                ar.playerStats = ps;
            }
            if(newRoom.doorExit != null)
            {
                newRoom.doorExit.index = newRoom.Index * 2;
            }
            if(newRoom.doorEnter != null)
            {
                newRoom.doorEnter.index = newRoom.Index * 2 + 1;
            }

        }
        Doors.Add(null);
        DoorsOnSpawn = GameObject.FindGameObjectsWithTag("Door");
        for (int i = 0; i < DoorsOnSpawn.Length; i++)
        {
            for (int j = 0; j < DoorsOnSpawn.Length; j++)
            {
                int id = DoorsOnSpawn[j].GetComponent<Door>().index;
                if (id == i)
                {
                    Doors.Add(DoorsOnSpawn[j]);

                }
            }

        }
        Doors.Add(DoorsOnSpawn[DoorsOnSpawn.Length - 1]);


    }


}
