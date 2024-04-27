using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [Header("Damage")]
    [SerializeField] private int damage;
    [SerializeField] private float knockDuration;
    [SerializeField] private float knockPower;


    void Update()
    {
        MovingThings();
    }

    private void MovingThings()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);

            StartCoroutine(PlayerHealth.instance.Knockback(knockDuration, knockPower, this.transform));
        }
    }
}
