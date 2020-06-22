using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Controlls;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] public GameObject bullet = null;
    public Transform bulletSpawnLocation = null;
    [SerializeField] public float shootRecharge = 2;
    [SerializeField] public float bulletLifeInSeconds = 2;

    [SerializeField] public bool listenInput = false;
    private float lastTimeShoot = 0;
    public static Action<Vector3> OnPlayerShoot;

    private void Update()
    {
        if ( listenInput && Input.GetKey(fireKey))
        {
            float currTime = Time.time;
            if (currTime - lastTimeShoot > shootRecharge)
            {
                Shoot();
                OnPlayerShoot?.Invoke(bulletSpawnLocation.position);
                lastTimeShoot = currTime;
            }

        }
    }
    virtual public void Shoot()
    { 
    }
}
