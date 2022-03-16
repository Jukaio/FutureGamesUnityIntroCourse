using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    [SerializeField] private string despawnerTag = "Despawner";

    private void OnTriggerEnter2D(Collider2D other) // NOT OnCollisionEnter2D!!
    {
        if (other.CompareTag(despawnerTag)) {
            Destroy(this.gameObject);
        }
    }
}
