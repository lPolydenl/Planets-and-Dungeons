using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    [SerializeField] private RoomSpawner[] roomSpawners;
    [SerializeField] private AudioSource[] planetMusic;
    [SerializeField] private PlayerStats ps;
    private RoomSpawner rs;
    private AudioSource pm;
    private void Awake()
    {
        rs = Instantiate(roomSpawners[PlayerPrefs.GetInt("Planet")]);
        pm = Instantiate(planetMusic[PlayerPrefs.GetInt("Planet")]);
        rs.music = pm;
        rs.ps = ps;
    }
}
