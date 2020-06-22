using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private PlayerGun gun = null;
    private Transform bulletSpawnLocation = null;

    [SerializeField] private float speed = 10;
    [SerializeField] public float shootRecharge = 2;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        bulletSpawnLocation = transform.GetChild(0);
    }

    void Update()
    {
        LookAtPlayer();
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        Vector3 VectorToTarget = (targetPosition - transform.position).normalized;
        transform.position += VectorToTarget * Time.deltaTime * speed;
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
