using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public Image key;

    private void Awake()
    {
        key.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectables"))
        {
            Destroy(collision.gameObject);

            key.enabled = true;

            FindObjectOfType<AudioManager>().Play("Key");
        }
    }
}
