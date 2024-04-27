using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] private float distance;
    
    private bool isFalling;
    
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if(hit.transform != null)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else
            {
                rb.gravityScale = 0;
                boxCollider2D.enabled = false;
            }
        }
    }
}
