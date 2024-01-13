using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] GameObject optionsScreen;

    public void ShowScreen()
    {
        optionsScreen.SetActive(true);
    }
    public void HideScreen()
    {
        optionsScreen.SetActive(false);
    }
}
