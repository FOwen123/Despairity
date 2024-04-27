using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite doorOpen;
    private bool doorOpened;

    private SpriteRenderer spriteRend;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (doorOpened)
        {
            NextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<ItemCollector>().key.enabled)
            {
                spriteRend.sprite = doorOpen;
                doorOpened = true;

                collision.GetComponent<ItemCollector>().key.enabled = false;
            }
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        FindObjectOfType<AudioManager>().Play("Next Level");
    }
}
