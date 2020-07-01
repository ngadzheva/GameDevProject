using UnityEngine;
using System;
using static Controlls;
using static AudioManager;
using static PlayerWeaponInventory;

public class Pistol : PlayerGun
{
    private float lastTimeShoot = 0;

    private void Start()
    {
        ammoType = AmmoType.pistolAmmo;
        bulletSpawnLocation = transform.Find("Weapon").GetChild(0);
    }

    override public void Shoot()
    {
        Vector3 direction = (bulletSpawnLocation.position - transform.position).normalized;

        Bullet bulletInstance = Instantiate(bullet,
            bulletSpawnLocation.position,
            Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.right, direction, Vector3.forward), Vector3.forward)).GetComponent<Bullet>();
        PlayGunfireSound();

        bulletInstance.MoveDirection = direction;

        Destroy(bulletInstance.gameObject, bulletLifeInSeconds);
    }

}
