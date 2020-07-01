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
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * (bulletSpawnLocation.position - transform.position).normalized;

            Bullet bulletInstance = Instantiate(bullet, 
                bulletSpawnLocation.position,
                Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.right, direction, Vector3.forward), Vector3.forward)).GetComponent<Bullet>();

            bulletInstance.MoveDirection = direction;

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
