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


    public static int Ammo1 { get; set; } = 10;
    public static int Ammo2 { get; set; } = 10;
    public static int Ammo3 { get; set; } = 10;

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
                if (Ammo1 > 0)
                    result = true;
                break;
            case AmmoType.rifleAmmo:
                if (Ammo2 > 0)
                    result = true;
                break;
            case AmmoType.shotgunAmmo:
                if (Ammo3 > 0)
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
                Ammo1 -= n;
                break;
            case AmmoType.rifleAmmo:
                Ammo2 -= n;
                break;
            case AmmoType.shotgunAmmo:
                Ammo3 -= n;
                break;
        }
    }

    public static void AmmoAdd(AmmoType type, int n)
    {
        switch (type)
        {
            case AmmoType.pistolAmmo:
                Ammo1 += n;
                break;
            case AmmoType.rifleAmmo:
                Ammo2 += n;
                break;
            case AmmoType.shotgunAmmo:
                Ammo3 += n;
                break;
        }
    }
}
