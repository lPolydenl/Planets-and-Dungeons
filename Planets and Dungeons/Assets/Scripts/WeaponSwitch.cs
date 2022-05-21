using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private int armsCount;
    [SerializeField] private int selectedWeapon;
    void Start()
    {
        selectedWeapon = armsCount;
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = armsCount;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= armsCount)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = armsCount;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 + armsCount)
        {
            selectedWeapon = armsCount + 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i >= armsCount)
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                    weapon.gameObject.GetComponent<Weapon>().isSelected = true;
                }

                else
                {
                    weapon.gameObject.SetActive(false);
                    weapon.gameObject.GetComponent<Weapon>().isSelected = false;
                }
            }
            i++;
        }
    }
}
