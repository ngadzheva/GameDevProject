using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static Controlls;

public class PlayerWeaponInventory : MonoBehaviour
{
    private GameObject playerWeapon = null;

    [SerializeField] private GameObject weapon1 = null;
    [SerializeField] private GameObject weapon2 = null;
    [SerializeField] private GameObject weapon3 = null;


    public static int ammo1 = 10;
    public static int ammo2 = 10;
    public static int ammo3 = 10;

    public enum AmmoType
    {
        pistolAmmo,
        shotgunAmmo,
        rifleAmmo
    }


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

    public static bool HasAmmo(AmmoType type)
    {
        bool result = false;
        switch (type)
        {
            case AmmoType.pistolAmmo:
                if (ammo1 > 0)
                    result = true;
                break;
            case AmmoType.rifleAmmo:
                if (ammo2 > 0)
                    result = true;
                break;
            case AmmoType.shotgunAmmo:
                if (ammo3 > 0)
                    result = true;
                break;
        }

        return result;
    }

    public static void AmmoSubstract(AmmoType type, int n=1)
    {
        switch (type)
        {
            case AmmoType.pistolAmmo:
                ammo1 -= n;
                break;
            case AmmoType.rifleAmmo:
                ammo2 -= n;
                break;
            case AmmoType.shotgunAmmo:
                ammo3 -= n;
                break;
        }
    }

    public static void AmmoAdd(AmmoType type, int n)
    {
        switch (type)
        {
            case AmmoType.pistolAmmo:
                ammo1 += n;
                break;
            case AmmoType.rifleAmmo:
                ammo2 += n;
                break;
            case AmmoType.shotgunAmmo:
                ammo3 += n;
                break;
        }
    }
}
