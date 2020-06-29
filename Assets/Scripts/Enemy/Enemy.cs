using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private PlayerGun gun = null;
    private Transform bulletSpawnLocation = null;

    [SerializeField] private float speed = 10;
    [SerializeField] public float shootRecharge = 2;
    private Transform player;

    public static Func<float> TimeModif;

    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        bulletSpawnLocation = transform.GetChild(0);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        LookAtPlayer();
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        float timeModif = 1f;
        float? newTineModif = TimeModif?.Invoke();
        if (newTineModif.HasValue)
        {
            timeModif = newTineModif.Value;
        }

        Vector3 VectorToTarget = (targetPosition - transform.position).normalized;
        transform.position += VectorToTarget * Time.deltaTime * speed * timeModif;
        transform.right = VectorToTarget;
    }

    public void Shoot()
    {
        //Bullet bulletInstance = Instantiate(bullet,
        //    bulletSpawnLocation.position,
        //    Quaternion.identity).GetComponent<Bullet>();

        //bulletInstance.MoveDirection = (bulletSpawnLocation.position - transform.position).normalized;

        //Destroy(bulletInstance.gameObject, 3);
        gun.Shoot();
    }

    private void LookAtPlayer()
    {
        Vector3 vectorToPlayer = (player.position - transform.position).normalized;
        transform.right = vectorToPlayer;
    }
}
