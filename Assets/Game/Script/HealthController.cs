using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int startingHealthPoints = 3;
    [SerializeField] private string lethalBulletTag = "ENTER LETHAL BULLET TAG HERE";
    [SerializeField] private ExplosionController explosionPrefab;
    [SerializeField] private AudioSource deathSoundPrefab;
    [SerializeField] private bool isPlayerHealthController = false;
    [SerializeField] private UnityEvent onDeathEvent; // Set the events in the inspector through this line of code

    private int currentHealthPoints = 0;

    private KillCounterUI killCounterUI = null;

    public int GetCurrentHealth()
    {
        return currentHealthPoints;
    }

    private void Start()
    {
        currentHealthPoints = startingHealthPoints;
        killCounterUI = FindObjectOfType<KillCounterUI>();
    }


    public void ReceiveDamage(int reduceHealthBy)
    {
        if (currentHealthPoints > 0) {
            currentHealthPoints -= reduceHealthBy;
        }

        if (currentHealthPoints <= 0) {
            Die();
        }
    }

    private void Die()
    {
        // Call every function that is in the list of the death event
        onDeathEvent.Invoke();

        if (isPlayerHealthController == false) {
            killCounterUI.CountUp();
        }

        Destroy(this.gameObject);

        var explosionInstance = Instantiate(explosionPrefab);
        explosionInstance.transform.position = transform.position;

        var deathSoundInstance = Instantiate(deathSoundPrefab);
        deathSoundInstance.transform.position = transform.position;
    }

    // Other == what we collide with
    private void OnTriggerEnter2D(Collider2D other) // NOT OnCollisionEnter2D!!
    {
        const int DAMAGE_POINTS = 1;

        if(other.CompareTag(lethalBulletTag)) {
            ReceiveDamage(DAMAGE_POINTS);
        }
    }
}
