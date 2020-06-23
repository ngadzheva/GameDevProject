using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static Controlls;

public class PlayerWeaponSelect : MonoBehaviour
{
    private GameObject playerWeapon = null;

    [SerializeField] private GameObject weapon1 = null;
    [SerializeField] private GameObject weapon2 = null;
    [SerializeField] private GameObject weapon3 = null;


    void Start()
    {
        
    }

    void Update()
    {
        playerWeapon = transform.Find("Weapon").gameObject;
        if (Input.GetKeyDown(Weapon1))
        {
            Destroy(playerWeapon);
            GameObject newWeapon = Instantiate(weapon1, transform);
            newWeapon.name = "Weapon";
        }
        else if (Input.GetKeyDown(Weapon2))
        {
            Destroy(playerWeapon);
            GameObject newWeapon = Instantiate(weapon2, transform);
            newWeapon.name = "Weapon";
        }
        else if (Input.GetKeyDown(Weapon3))
        {
            Destroy(playerWeapon);
            GameObject newWeapon = Instantiate(weapon3, transform);
            newWeapon.name = "Weapon";
        }
    }
}
