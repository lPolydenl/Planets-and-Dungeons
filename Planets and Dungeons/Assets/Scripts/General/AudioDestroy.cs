using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour
{
    float timeUntilDestroy = 2f;
    void Update()
    {
        if(timeUntilDestroy <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeUntilDestroy -= Time.deltaTime;
        }
    }
}
