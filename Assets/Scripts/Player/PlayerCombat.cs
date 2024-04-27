using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;

    public Animator playerAnimCombat;

    public bool isAttacking = false;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    [SerializeField] private int damage;

    [SerializeField] private float attackRate;
    [SerializeField] private float nextAttack;

    private void Awake()
    {
        playerAnimCombat = GetComponent<Animator>();
        instance = this;
    }

    void Update()
    {
        if(Time.time >= nextAttack)
        {
            if (Input.GetMouseButtonDown(0) && !isAttacking)
            {
                isAttacking = true;
                Attack();

                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    private void Attack()
    {


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<Enemy>().TakeDamageEnemy(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
