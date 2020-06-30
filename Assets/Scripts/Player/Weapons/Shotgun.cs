using UnityEngine;
using System;
using static Controlls;
using static AudioManager;
using static PlayerWeaponInventory;

public class Shotgun : PlayerGun
{
    private float lastTimeShoot = 0;
    public int pellets = 3;
    public float distanceInDegrees = 15f;


    private void Start()
    {
        ammoType = AmmoType.shotgunAmmo;
        bulletSpawnLocation = transform.Find("Weapon").GetChild(0);
    }
    override public void Shoot()
    {
        float angle = 0;
        for ( int i = 0; i < pellets; i++ )
        {
            Bullet bulletInstance = Instantiate(bullet, bulletSpawnLocation.position, Quaternion.identity).GetComponent<Bullet>();

            bulletInstance.MoveDirection = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * (bulletSpawnLocation.position - transform.position).normalized;

            Destroy(bulletInstance.gameObject, bulletLifeInSeconds);

            if( i % 2 == 0 )
            {
                angle = angle + distanceInDegrees * (i + 1);
            }
            else
            {
                angle = angle - distanceInDegrees * (i + 1);
            }

        }

    }

}
