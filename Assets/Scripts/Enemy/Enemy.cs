using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update()
    {
       
    }

    public void TakeDamageEnemy(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // play die animation

        // disable the enemy
    }
}
