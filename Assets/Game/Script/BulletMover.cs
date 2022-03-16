using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private string opponentTag = "REPLACE WITH THE TAG OF THE BULLET'S OPPONENT";

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Vector3 direction;
    [SerializeField] private AudioSource impactSoundPrefab;
    [SerializeField] private ExplosionController explosionPrefab;

    void Update()
    {
        transform.position = transform.position + direction.normalized * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) // NOT OnCollisionEnter2D!!
    {
        if (other.CompareTag(opponentTag)) {
            var explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.position = transform.position;

            var impactSoundInstance = Instantiate(impactSoundPrefab);
            impactSoundInstance.transform.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
