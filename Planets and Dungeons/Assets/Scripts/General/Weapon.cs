using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite weaponIcon;
    [HideInInspector] public bool isSelected;
    [SerializeField] private Image weaponImage;
    
    private void Start()
    {
        weaponImage = GameObject.Find("/Canvas/Weapon slot/Image").GetComponent<Image>();
    }
    void Update()
    {
        if(isSelected)
        {
            weaponImage.sprite = weaponIcon;
        }
    }

}
