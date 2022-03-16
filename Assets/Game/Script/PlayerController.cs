using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// {protection} {class(and other)} {name} : {base}
// protection = Protection level: public, protected, private
// class(and other) = what kind of data type? class (or struct)
// name = Name of the class
// base = Base/Interfaces
// Classes contain data (variables) and method/functions

// Function declarations looks like the following:
// {protection} {return type} {name} ({parameters})
// { 
//    .. // scope, function/method body
// }
public class PlayerController : MonoBehaviour
{
    [Header("Balancing")]
    [SerializeField] private float speed = 10.0f; // Because speed of 0 would be awkward
    [SerializeField] private bool canMoveVertically = false;
    [SerializeField] private bool canMoveHorizontally = true;

    [Header("Controls")]
    [SerializeField] private Key moveUpKey = Key.W;
    [SerializeField] private Key moveDownKey = Key.S;
    [SerializeField] private Key moveLeftKey = Key.A;
    [SerializeField] private Key moveRightKey = Key.D;
    [SerializeField] private Key shootKey = Key.Space;

    [Header("Bullet Data")]
    [SerializeField] private /* or public */ BulletMover bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private bool canShootContinuous = true;

    [Header("Other")]
    [SerializeField] private string enemyTag = "Enemy";

    private Rigidbody2D rb = null;
    private HealthController health = null;
    private float fireRateTimer = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthController>();
    }

    // velocity == Vector.zero means no movement
    private void SetVelocity(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        var leftKey = Keyboard.current[moveLeftKey];
        var rightKey = Keyboard.current[moveRightKey];
        var upKey = Keyboard.current[moveUpKey];
        var downKey = Keyboard.current[moveDownKey];

        Vector2 inputDirection = Vector2.zero;
        if (canMoveHorizontally == true) {
            if (leftKey.isPressed) {
                inputDirection = inputDirection + Vector2.left;
            }
            if (rightKey.isPressed) {
                inputDirection = inputDirection + Vector2.right;
            }
        }
        if (canMoveVertically == true) {
            if (upKey.isPressed) {
                inputDirection = inputDirection + Vector2.up;
            }
            if (downKey.isPressed) {
                inputDirection = inputDirection + Vector2.down;
            }
        }
        inputDirection.Normalize();
        SetVelocity(inputDirection);
    }

    private void Shoot()
    {
        if(fireRateTimer > 0.0f) { 
            fireRateTimer -= Time.deltaTime; 
            return;
        }

        var key = Keyboard.current[shootKey];
        bool isPressed;
        if (canShootContinuous == true) {
            isPressed = key.isPressed;
        }
        else /* isShootingContinuous == false */ {
            isPressed = key.wasPressedThisFrame;
        }

        if(isPressed) {
            fireRateTimer = fireRate;
            var bulletInstance = Instantiate(bulletPrefab); // Make copy
            // bulletSpawnPoint.position == this.transform.position + some offset
            bulletInstance.transform.position = bulletSpawnPoint.position; // set position to player position
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(enemyTag)) {
            // Good bye player
            health.ReceiveDamage(health.GetCurrentHealth());
        }
    }
}
