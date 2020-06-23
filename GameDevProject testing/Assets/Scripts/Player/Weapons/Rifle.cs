﻿using UnityEngine;
using System;
using static Controlls;
using static AudioManager;

public class Rifle : PlayerGun
{
    private float lastTimeShoot = 0;


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
