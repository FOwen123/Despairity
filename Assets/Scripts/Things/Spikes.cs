using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float knockDuration;
    [SerializeField] private float knockPower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);

            StartCoroutine(PlayerHealth.instance.Knockback(knockDuration, knockPower, this.transform));
        }
    }

}
