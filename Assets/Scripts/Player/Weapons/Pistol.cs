﻿using UnityEngine;
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
        Bullet bulletInstance = Instantiate(bullet,
            bulletSpawnLocation.position,
            Quaternion.identity).GetComponent<Bullet>();
        PlayGunfireSound();

        bulletInstance.MoveDirection = (bulletSpawnLocation.position - transform.position).normalized;

        Destroy(bulletInstance.gameObject, bulletLifeInSeconds);
    }

}
