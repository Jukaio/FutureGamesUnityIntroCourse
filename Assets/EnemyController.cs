using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Bullet Data")]
    [SerializeField] private /* or public */ BulletMover bulletPrefab;
    [SerializeField] private Transform leftBulletSpawnPoint;
    [SerializeField] private Transform rightBulletSpawnPoint;
    [SerializeField] private float fireRate = 1.0f;

    [Header("Movement Data")]
    [SerializeField] private float speed = 2.0f;

    private Rigidbody2D rb = null;
    private float fireRateTimer = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = Vector3.down * speed;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (fireRateTimer > 0.0f) {
            fireRateTimer -= Time.deltaTime;
            return;
        }
        fireRateTimer = fireRate;

        var leftBulletInstance = Instantiate(bulletPrefab);
        leftBulletInstance.transform.position = leftBulletSpawnPoint.position;

        var rightBulletInstance = Instantiate(bulletPrefab);
        rightBulletInstance.transform.position = rightBulletSpawnPoint.position;
    }
}
