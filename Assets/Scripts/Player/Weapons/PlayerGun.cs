using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Controlls;
using static PlayerWeaponInventory;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] public GameObject bullet = null;
    public Transform bulletSpawnLocation = null;
    [SerializeField] public float shootRecharge = 2;
    [SerializeField] public float bulletLifeInSeconds = 2;

    [SerializeField] public bool listenInput = true;
    private float lastTimeShoot = 0;

    public static AmmoType ammoType;
    public static Action<Vector3> OnPlayerShoot;

    public static Func<float> TimeModif;

    private void Start()
    {
        bulletSpawnLocation = transform.Find("Weapon").GetChild(0);
    }

    private void Update()
    {
        if ( listenInput && Input.GetKey(fireKey) && HasAmmo(ammoType))
        {
            float timeModif = 1f;
            float? newTineModif = TimeModif?.Invoke();
            if (newTineModif.HasValue)
            {
                timeModif = newTineModif.Value;
            }


            float currTime = Time.time;
            if (currTime - lastTimeShoot > shootRecharge / timeModif)
            {
                Shoot();
                AmmoSubstract(ammoType);
                OnPlayerShoot?.Invoke(bulletSpawnLocation.position);
                lastTimeShoot = currTime;
            }

        }
    }
    virtual public void Shoot()
    {
    }
}
