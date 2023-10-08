using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    public int grenades = 3;
    [SerializeField] private int maxGrenades = 3;
    public void RemoveGrenade()
    {
        grenades--;
    }
    public void AddGrenades(int gr)
    {
        grenades += gr;
        if(grenades > maxGrenades)
        {
            grenades = maxGrenades;
        }
    }
}
