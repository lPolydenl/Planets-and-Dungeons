using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private void Awake()
    {
        Instantiate(characters[PlayerPrefs.GetInt("Character")], transform.position, Quaternion.identity);
    }
}
